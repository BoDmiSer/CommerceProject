using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Commerce.Domain;
using Commerce.Domain.EventHandlers;
using Commerce.SqlDataAccess;
using Commerce.Web.Presentation;
using System.Linq;
using System.Reflection;

namespace Commerce.Web.Autofac
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings");

            this.Configuration = new CommerceConfiguration(
                connectionString: configuration.GetConnectionString("CommerceConnectionString"));
        }

        public CommerceConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly domainAssembly = typeof(ITimeProvider).Assembly;
            builder.RegisterInstance<IUserContext>(new AspNetUserContextAdapter());
            builder.Register(c => new CommerceContext(this.Configuration.ConnectionString))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(domainAssembly).AsClosedTypesOf(typeof(ICommandService<>));

            builder.RegisterAssemblyTypes(domainAssembly)
                .As(type =>
                    from interfaceType in type.GetInterfaces()
                    where interfaceType.IsClosedTypeOf(typeof(IEventHandler<>))
                    select new KeyedService("handler", interfaceType));

            builder.RegisterGeneric(typeof(CompositeEventHandler<>))
                .As(typeof(IEventHandler<>))
                .WithParameter(
                    (p, c) => true,
                    (p, c) => c.ResolveNamed("handler", p.ParameterType));

            builder.RegisterAssemblyTypes(typeof(SqlProductRepository).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
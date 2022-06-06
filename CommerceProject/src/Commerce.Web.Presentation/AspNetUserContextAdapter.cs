using Microsoft.AspNetCore.Http;
using Commerce.Domain;
using System;

namespace Commerce.Web.Presentation
{
    public class AspNetUserContextAdapter : IUserContext
    {
        private static readonly HttpContextAccessor Accessor = new HttpContextAccessor();

        public Guid CurrentUserId => Guid.NewGuid();

        public bool IsInRole(Role role) => true;
    }
}
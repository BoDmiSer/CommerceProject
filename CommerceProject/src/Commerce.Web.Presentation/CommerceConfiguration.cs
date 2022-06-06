using System;

namespace Commerce.Web.Presentation
{
    public class CommerceConfiguration
    {
        public readonly string ConnectionString;
        public readonly Type ProductRepositoryType;

        public CommerceConfiguration(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            this.ConnectionString = connectionString;
        }
    }
}

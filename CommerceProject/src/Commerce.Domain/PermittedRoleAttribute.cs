using System;

namespace Commerce.Domain
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PermittedRoleAttribute : Attribute
    {
        public readonly Role Role;

        public PermittedRoleAttribute(Role role)
        {
            this.Role = role;
        }
    }
}
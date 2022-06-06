using System;

namespace Commerce.Domain
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        bool IsInRole(Role role);
    }
}
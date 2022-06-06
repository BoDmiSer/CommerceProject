using System;

namespace Commerce.Domain
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
using System;

namespace Commerce.Domain
{
    public interface IInventoryRepository
    {
        ProductInventory GetByIdOrNull(Guid id);
        void Save(ProductInventory productInventory);
    }
}
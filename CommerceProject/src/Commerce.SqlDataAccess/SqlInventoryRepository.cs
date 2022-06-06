using Commerce.Domain;
using System;

namespace Commerce.SqlDataAccess
{
    public class SqlInventoryRepository : IInventoryRepository
    {
        private readonly CommerceContext context;

        public SqlInventoryRepository(CommerceContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            this.context = context;
        }

        public ProductInventory GetByIdOrNull(Guid id)
        {
            return this.context.ProductInventories.Find(id);
        }

        public void Save(ProductInventory productInventory)
        {
            if (productInventory == null) throw new ArgumentNullException(nameof(productInventory));

            if (this.context.IsNew(productInventory))
            {
                this.context.ProductInventories.Add(productInventory);
            }
        }
    }
}
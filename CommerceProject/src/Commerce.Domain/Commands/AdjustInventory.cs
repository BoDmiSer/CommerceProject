using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.Commands
{
    [PermittedRole(Role.InventoryManager)]
    public class AdjustInventory
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }

        [DisplayName("Снизить")]
        public bool Decrease { get; set; }

        [DisplayName("Количество")]        
        [Range(minimum: 1, maximum: 10000)]
        public int Quantity { get; set; }
    }
}
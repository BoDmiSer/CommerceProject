using Microsoft.AspNetCore.Mvc.Rendering;
using Commerce.Domain.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commerce.Web.Presentation.Models
{
    public class AdjustInventoryViewModel
    {
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> DecreaseOptions { get; set; }

        [Required]
        public AdjustInventory Command { get; set; }
    }
}
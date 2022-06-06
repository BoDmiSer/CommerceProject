﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Commerce.Domain;
using Commerce.Domain.Commands;
using Commerce.Web.Presentation.Models;
using System;
using System.Linq;

namespace Commerce.Web.Presentation.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductRepository repository;
        private readonly ICommandService<AdjustInventory> inventoryAdjuster;

        public InventoryController(
            IProductRepository repository, ICommandService<AdjustInventory> inventoryAdjuster)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (inventoryAdjuster == null) throw new ArgumentNullException(nameof(inventoryAdjuster));

            this.repository = repository;
            this.inventoryAdjuster = inventoryAdjuster;
        }

        [Route("inventory/")]
        public ActionResult Index()
        {
            return this.View(this.Populate(new AdjustInventoryViewModel()));
        }

        [Route("inventory/adjustinventory")]
        public ActionResult AdjustInventory(AdjustInventoryViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(nameof(Index), this.Populate(viewModel));
            }

            AdjustInventory command = viewModel.Command;

            this.inventoryAdjuster.Execute(command);

            this.TempData["SuccessMessage"] = "Инвентарь успешно скорректирован";

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private AdjustInventoryViewModel Populate(AdjustInventoryViewModel vm)
        {
            vm.Products =
                from product in this.repository.GetAll()
                select new SelectListItem(product.Name, product.Id.ToString());

            vm.DecreaseOptions = new[]
            {
                new SelectListItem("Да", bool.TrueString),
                new SelectListItem("нет", bool.FalseString)
            };

            return vm;
        }
    }
}
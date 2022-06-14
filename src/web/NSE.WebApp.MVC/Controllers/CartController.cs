using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IPurchasesBffService _purchasesBffService;

        public CartController(IPurchasesBffService purchasesBffService)
        {
            _purchasesBffService = purchasesBffService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _purchasesBffService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddNewItemCart(ItemCartViewModel itemCart)
        {
            var response = await _purchasesBffService.AddItemToCart(itemCart);
            if (ResponseAnyErrors(response)) return View("Index", await _purchasesBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            var itemProduct = new ItemCartViewModel() {ProductId = productId, Quantity = quantity};
            var response = await _purchasesBffService.UpdateItemFromCart(productId, itemProduct);

            if (ResponseAnyErrors(response)) return View("Index", await _purchasesBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            var response = await _purchasesBffService.RemoveItemFromCart(productId);
            if (ResponseAnyErrors(response)) return View("Index", await _purchasesBffService.GetCart());

            return RedirectToAction("Index");
        }
    }
}

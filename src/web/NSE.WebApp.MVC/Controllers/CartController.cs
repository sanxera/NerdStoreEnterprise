using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService cartService, ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddNewItemCart(ItemProductViewModel itemProduct)
        {
            var product = await _catalogService.FindById(itemProduct.ProductId);

            ValidateCartItem(product, itemProduct.Quantity);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            itemProduct.Name = product.Name;
            itemProduct.Value = product.Value;
            itemProduct.Image = product.Image;

            var response = await _cartService.AddItemToCart(itemProduct);

            if (ResponseAnyErrors(response)) return View("Index", await _cartService.GetCart());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            var product = await _catalogService.FindById(productId);

            ValidateCartItem(product, quantity);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            var itemProduct = new ItemProductViewModel() {ProductId = productId, Quantity = quantity};
            var response = await _cartService.UpdateItemFromCart(productId, itemProduct);

            if (ResponseAnyErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            var product = await _catalogService.FindById(productId);

            if (product == null)
            {
                AddErrorValidation("Produto inexistente !");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveItemFromCart(productId);
            if (ResponseAnyErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        private void ValidateCartItem(ProductViewModel product, int quantity)
        {
            if (product == null) AddErrorValidation("Produto inexistente!");
            if (quantity < 1) AddErrorValidation($"Escolha ao menos uma unidade do product {product.Name}");
            if (quantity > product.InventoryQuantity) AddErrorValidation($"O product {product.Name} possui {product.InventoryQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}

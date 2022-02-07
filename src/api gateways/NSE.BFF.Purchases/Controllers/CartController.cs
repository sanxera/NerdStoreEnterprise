using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSE.BFF.Purchases.Models;
using NSE.BFF.Purchases.Services.Interfaces;
using NSE.WebAPI.Core.Controllers;

namespace NSE.BFF.Purchases.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, ICatalogService catalogService, IOrderService orderService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("purchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("purchases/cart-quantity")]
        public async Task<int> GetCartQuantity()
        {
            var cart = await _cartService.GetCart();
            return cart?.Items.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("purchases/cart/items")]
        public async Task<IActionResult> AddNewCartItem(ItemCartDto itemProduct)
        {
            var product = await _catalogService.FindById(itemProduct.ProductId);

            await ValidateCartItem(product, itemProduct.Quantity);
            if (!ValidOperation()) return CustomResponse();

            itemProduct.Name = product.Name;
            itemProduct.Value = product.Value;
            itemProduct.Image = product.Image;

            var response = await _cartService.AddItemToCart(itemProduct);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("purchases/cart/items/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, ItemCartDto itemProduct)
        {
            var product = await _catalogService.FindById(productId);

            await ValidateCartItem(product, itemProduct.Quantity);
            if (!ValidOperation()) return CustomResponse();

            var response = await _cartService.UpdateItemFromCart(productId, itemProduct);

            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("purchases/cart/items/{productId}")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            var product = await _catalogService.FindById(productId);

            if (product == null)
            {
                AddErrorProcessing("Product not exists");
                return CustomResponse();
            }

            var response = await _cartService.RemoveItemFromCart(productId);

            return CustomResponse();
        }

        [HttpPost]
        [Route("purchases/cart/apply-voucher")]
        public async Task<IActionResult> AddVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.FindVoucherByCode(voucherCode);

            if (voucher is null)
            {
                AddErrorProcessing("Invalid voucher !");
                return CustomResponse();
            }

            var response = await _cartService;

            return CustomResponse(response);
        }

        private async Task ValidateCartItem(ItemProductDto product, int quantity)
        {
            if (product == null) AddErrorProcessing("Produto inexistente!");
            if (quantity < 1) AddErrorProcessing($"Escolha ao menos uma unidade do produto {product.Name}");

            var cart = await _cartService.GetCart();
            var itemCart = cart.Items.FirstOrDefault(p => p.ProductId == product.Id);

            if (product != null && itemCart != null && itemCart.Quantity + quantity > product.StockQuantity)
            {
                AddErrorProcessing($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
            }

            if (quantity > product.StockQuantity) AddErrorProcessing($"O product {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}

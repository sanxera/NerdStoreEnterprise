using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Cart.API.Data;
using NSE.Cart.API.Model;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _cartContext;

        public CartController(IAspNetUser user, CartContext cartContext)
        {
            _user = user;
            _cartContext = cartContext;
        }

        [HttpGet("cart")]
        public async Task<CartClient> GetCart()
        {
            return await GetClientCart() ?? new CartClient();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddNewItemCart(CartItem item)
        {
            var cart = await GetClientCart();

            if (cart == null)
                ManipulateNewCart(item);
            else
                ManipulateExistingCart(cart, item);

            if (!ValidOperation()) return CustomResponse();

            await SaveChanges();
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var cart = await GetClientCart();
            var itemCart = await GetCartItemValidated(productId, cart, item);
            if (itemCart == null) return CustomResponse();

            cart.UpdateUnits(itemCart, item.Quantity);

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            _cartContext.CartItems.Update(itemCart);
            _cartContext.CartClient.Update(cart);

            await SaveChanges();
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            var cart = await GetClientCart();
            var itemCart = await GetCartItemValidated(productId, cart);
            if (itemCart == null) return CustomResponse();

            cart.RemoveItem(itemCart);

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            _cartContext.CartItems.Remove(itemCart);
            _cartContext.CartClient.Remove(cart);

            await SaveChanges();
            return CustomResponse();
        }

        private bool ValidateCart(CartClient cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddErrorProcessing(e.ErrorMessage));
            return false;
        }

        private async Task SaveChanges()
        {
            var result = await _cartContext.SaveChangesAsync();
            if (result <= 0) AddErrorProcessing("Erro ao salvar informações");
        }

        private async Task<CartItem> GetCartItemValidated(Guid productId, CartClient cart, CartItem item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddErrorProcessing("O item não corresponde ao informado");
                return null;
            }

            if (cart == null)
            {
                AddErrorProcessing("Carrinho não encontrado");
                return null;
            }

            var itemCart = await _cartContext.CartItems
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.ItemExistInCart(itemCart))
            {
                AddErrorProcessing("O item não está no cart");
                return null;
            }

            return itemCart;
        }


        private void ManipulateNewCart(CartItem item)
        {
            var cart = new CartClient(_user.GetUserId());
            cart.AddNewItem(item);

            ValidateCart(cart);
            _cartContext.CartClient.Add(cart);
        }

        private void ManipulateExistingCart(CartClient cart, CartItem item)
        {
            var productExistsExisting = cart.ItemExistInCart(item);

            cart.AddNewItem(item);
            ValidateCart(cart);

            if (productExistsExisting)
                _cartContext.CartItems.Update(cart.GetByProductId(item.Id));
            else
                _cartContext.CartItems.Add(item);

            _cartContext.CartClient.Update(cart);
        }

        private async Task<CartClient> GetClientCart()
        {
            return await _cartContext.CartClient
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }
    }
}

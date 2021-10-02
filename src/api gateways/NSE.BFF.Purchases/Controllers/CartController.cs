using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPI.Core.Controllers;

namespace NSE.BFF.Purchases.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        [HttpGet]
        [Route("purchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("purchases/cart-quantity")]
        public async Task<IActionResult> GetCartQuantity()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("purchases/cart/items")]
        public async Task<IActionResult> AddNewCartItem()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("purchases/cart/items/{productId}")]
        public async Task<IActionResult> UpdateCartItem()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("purchases/cart/items/{productId}")]
        public async Task<IActionResult> DeleteCartItem()
        {
            return CustomResponse();
        }
    }
}

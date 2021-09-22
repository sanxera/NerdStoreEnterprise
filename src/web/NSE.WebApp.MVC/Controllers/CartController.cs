using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return null;
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddNewItemCart(ItemProductViewModel itemProduct)
        {
            return null;
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            return null;
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            return null;
        }
    }
}

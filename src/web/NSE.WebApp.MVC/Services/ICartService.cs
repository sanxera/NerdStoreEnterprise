using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Models.ResponseErrorViewModel;

namespace NSE.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddItemToCart(ItemProductViewModel product);
        Task<ResponseResult> UpdateItemFromCart(Guid productId, ItemProductViewModel product);
        Task<ResponseResult> RemoveItemFromCart(Guid productId);
    }
}

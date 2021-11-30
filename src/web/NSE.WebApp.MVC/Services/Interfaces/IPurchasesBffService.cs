using System;
using System.Threading.Tasks;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface IPurchasesBffService
    {
        Task<CartViewModel> GetCart();
        Task<int> GetCartQuantity();
        Task<ResponseResult> AddItemToCart(ItemCartViewModel cart);
        Task<ResponseResult> UpdateItemFromCart(Guid productId, ItemCartViewModel cart);
        Task<ResponseResult> RemoveItemFromCart(Guid productId);
    }
}

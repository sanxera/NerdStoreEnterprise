using System;
using System.Threading.Tasks;
using NSE.BFF.Purchases.Models;
using NSE.Core.Communication;

namespace NSE.BFF.Purchases.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCart();
        Task<ResponseResult> AddItemToCart(ItemCartDto product);
        Task<ResponseResult> UpdateItemFromCart(Guid productId, ItemCartDto product);
        Task<ResponseResult> RemoveItemFromCart(Guid productId);
        Task<ResponseResult> ApplyVoucher(VoucherDTO voucher);
    }
}

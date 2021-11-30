using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Services
{
    public class PurchasesBffService : Service, IPurchasesBffService
    {
        private readonly HttpClient _httpClient;

        public PurchasesBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.PurchasesBff);
            _httpClient = httpClient;
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/purchases/cart");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<CartViewModel>(response);
        }

        public async Task<int> GetCartQuantity()
        {
            var response = await _httpClient.GetAsync("/purchases/cart-quantity");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<int>(response);
        }

        public async Task<ResponseResult> AddItemToCart(ItemCartViewModel cart)
        {
            var itemContet = GetContent(cart);

            var response = await _httpClient.PostAsync("/purchases/cart/items", itemContet);

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemFromCart(Guid productId, ItemCartViewModel cart)
        {
            var itemContet = GetContent(cart);

            var response = await _httpClient.PutAsync($"/purchases/cart/items/{cart.ProductId}", itemContet);

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveItemFromCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/purchases/cart/items/{productId}");

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}

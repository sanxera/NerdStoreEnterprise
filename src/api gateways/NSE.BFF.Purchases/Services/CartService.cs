using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.BFF.Purchases.Extensions;
using NSE.BFF.Purchases.Models;
using NSE.BFF.Purchases.Services.Interfaces;
using NSE.Core.Communication;

namespace NSE.BFF.Purchases.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartDto> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<CartDto>(response);
        }

        public async Task<ResponseResult> AddItemToCart(ItemCartDto product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemFromCart(Guid productId, ItemCartDto product)
        {
            var itemContet = GetContent(product);

            var response = await _httpClient.PutAsync($"/cart/{product.ProductId}", itemContet);

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveItemFromCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> ApplyVoucher(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/cart/apply-voucher/", itemContent);

            if (!TreatErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}

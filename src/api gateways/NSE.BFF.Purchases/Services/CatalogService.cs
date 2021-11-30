using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.BFF.Purchases.Extensions;
using NSE.BFF.Purchases.Models;
using NSE.BFF.Purchases.Services.Interfaces;

namespace NSE.BFF.Purchases.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ItemProductDto> FindById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/product/{id}");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<ItemProductDto>(response);
        }

        public async Task<IEnumerable<ItemProductDto>> FindAll()
        {
            var response = await _httpClient.GetAsync("/catalog/products");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ItemProductDto>>(response);
        }
    }
}

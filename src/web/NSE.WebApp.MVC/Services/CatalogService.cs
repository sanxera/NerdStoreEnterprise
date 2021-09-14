using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<ProductViewModel> FindById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/product/{id}");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }

        public async Task<IEnumerable<ProductViewModel>> FindAll()
        {
            var response = await _httpClient.GetAsync("/catalog/products");

            TreatErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }
    }
}

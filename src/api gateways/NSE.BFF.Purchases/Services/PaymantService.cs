using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.BFF.Purchases.Extensions;
using NSE.BFF.Purchases.Services.Interfaces;

namespace NSE.BFF.Purchases.Services
{
    public class PaymantService : Service, IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymantService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PaymentUrl);
        }
    }
}

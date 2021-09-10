using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models.ResponseErrorViewModel;
using NSE.WebApp.MVC.Models.UserViewModel;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLogin),
                Encoding.UTF8,
                "application/json");

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.PostAsync("https://localhost:44353/api/identity/auth", loginContent);

            if (!TreatErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult =
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContet = new StringContent(
                JsonSerializer.Serialize(userRegister),
                Encoding.UTF8,
                "application/json");

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.PostAsync("https://localhost:44353/api/identity/new-account", registerContet);

            if (!TreatErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult =
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}

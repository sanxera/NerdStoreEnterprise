using System.Net.Http;
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
            var loginContent = GetContet(userLogin);
            
            var response = await _httpClient.PostAsync("https://localhost:44353/api/identity/auth", loginContent);

            if (!TreatErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContet = GetContet(userRegister);

            var response = await _httpClient.PostAsync("https://localhost:44353/api/identity/new-account", registerContet);

            if (!TreatErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }
    }
}

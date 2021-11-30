using System.Threading.Tasks;
using NSE.WebApp.MVC.Models.UserViewModel;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);

        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}

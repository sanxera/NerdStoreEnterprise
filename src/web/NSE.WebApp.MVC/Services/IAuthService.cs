using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models.UserViewModel;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<string> Login(UserLogin userLogin);

        Task<string> Register(UserRegister userRegister);
    }
}

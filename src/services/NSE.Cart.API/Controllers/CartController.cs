using Microsoft.AspNetCore.Authorization;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;

        public CartController(IAspNetUser user)
        {
            _user = user;
        }
    }
}

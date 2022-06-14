using Microsoft.AspNetCore.Mvc;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Client.API.Controllers
{
    public class ClientsController : MainController
    {
        [HttpGet("clients")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

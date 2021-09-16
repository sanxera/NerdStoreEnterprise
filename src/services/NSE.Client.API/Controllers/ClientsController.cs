using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

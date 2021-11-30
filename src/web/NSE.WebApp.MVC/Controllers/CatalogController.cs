using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.FindAll();

            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.FindById(id);

            return View(product);
        }
    }
}

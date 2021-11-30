using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPurchasesBffService _purchasesBffService;

        public CartViewComponent(IPurchasesBffService purchasesBffService)
        {
            _purchasesBffService = purchasesBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _purchasesBffService.GetCartQuantity());
        }
    }
}
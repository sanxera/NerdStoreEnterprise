using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class CartViewModel
    {
        public decimal TotalValue { get; set; }
        public List<ItemCartViewModel> Items { get; set; } = new List<ItemCartViewModel>();
    }
}

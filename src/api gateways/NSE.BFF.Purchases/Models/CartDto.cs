using System.Collections.Generic;

namespace NSE.BFF.Purchases.Models
{
    public class CartDto
    {
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartDto> Items { get; set; } = new List<ItemCartDto>();
    }
}
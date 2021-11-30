using System;

namespace NSE.BFF.Purchases.Models
{
    public class ItemProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
    }
}

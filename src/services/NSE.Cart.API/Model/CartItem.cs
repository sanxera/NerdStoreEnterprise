using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cart.API.Model
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string InventoryQuantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }
        public CartClient CartClient { get; set; }
    }
}

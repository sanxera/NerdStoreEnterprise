using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cart.API.Model
{
    public class CartClient
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public CartClient(Guid clientId)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
        }

        public CartClient() { }
    }
}

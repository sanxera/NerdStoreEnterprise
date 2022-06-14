using System;
using System.Text.Json.Serialization;
using FluentValidation;

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
        public int InventoryQuantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }

        [JsonIgnore]
        public CartClient CartClient { get; set; }

        internal void AssociateCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CaculateValue()
        {
            return InventoryQuantity * Value;
        }

        internal void AddNewUnits(int units)
        {
            InventoryQuantity += units;
        }

        internal void UpdateNewUnits(int units)
        {
            InventoryQuantity = units;
        }

        internal bool IsValid()
        {
            return new CartItemValidation().Validate(this).IsValid;
        }
    }

    public class CartItemValidation : AbstractValidator<CartItem>
    {
        public CartItemValidation()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.InventoryQuantity)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o {item.Name} é 1");

            RuleFor(c => c.InventoryQuantity)
                .LessThanOrEqualTo(CartClient.MAX_QUANTITY_ITEM)
                .WithMessage(item => $"A quantidade máxima do {item.Name} é {CartClient.MAX_QUANTITY_ITEM}");

            RuleFor(c => c.Value)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0");
        }
    }
}

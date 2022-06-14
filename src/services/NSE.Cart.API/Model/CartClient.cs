using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using NSE.Cart.API.Model.Enums;

namespace NSE.Cart.API.Model
{
    public class CartClient
    {
        internal const int MAX_QUANTITY_ITEM = 5;

        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal TotalValue { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public ValidationResult ValidationResult { get; set; }

        public bool UsedVoucher { get; set; }
        public decimal Discount { get; set; }
        public Voucher Voucher { get; set; }

        public CartClient(Guid clientId)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
        }

        public CartClient() { }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            UsedVoucher = true;
            CalculateCartValue();
        }

        private void CalculateTotalValueDiscount()
        {
            if (!UsedVoucher) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.DiscountType == TypeVoucherDiscount.Percent)
            {
                if (Voucher.Percent.HasValue)
                {
                    discount = (value * Voucher.Percent.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.ValueDiscount.HasValue)
                {
                    discount = Voucher.ValueDiscount.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        internal void AddNewItem(CartItem item)
        {
            item.AssociateCart(Id);
            
            if (ItemExistInCart(item))
            {
                var itemExists = GetByProductId(item.ProductId);
                itemExists.AddNewUnits(item.Quantity);

                item = itemExists;
                Items.Remove(itemExists);
            }

            Items.Add(item);
            CalculateCartValue();
        }

        internal void UpdateItem(CartItem item)
        {
            item.AssociateCart(Id);

            var itemExistent = GetByProductId(item.ProductId);

            Items.Remove(itemExistent);
            Items.Add(item);

            CalculateCartValue();
        }

        internal void UpdateUnits(CartItem item, int units)
        {
            item.UpdateNewUnits(units);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Items.Remove(GetByProductId(item.ProductId));

            CalculateCartValue();
        }

        internal void CalculateCartValue()
        {
            TotalValue = Items.Sum(p => p.CaculateValue());
            CalculateTotalValueDiscount();
        }

        internal bool ItemExistInCart(CartItem item)
        {
            return Items.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetByProductId(Guid produtoId)
        {
            return Items.FirstOrDefault(p => p.ProductId == produtoId);
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(x => new CartItemValidation().Validate(x).Errors).ToList();
            errors.AddRange(new CartClientValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }
    }

    public class CartClientValidation : AbstractValidator<CartClient>
    {
        public CartClientValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(c => c.TotalValue)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}

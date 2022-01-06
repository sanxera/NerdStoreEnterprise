using System;
using NSE.Core.DomainObjects;
using NSE.Core.DomainObjects.Interfaces;
using NSE.Order.Domain.Vouchers.Enums;
using NSE.Order.Domain.Vouchers.Specs;

namespace NSE.Order.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; set; }
        public decimal? Percent { get; set; }
        public decimal? ValueDiscount { get; set; }
        public int Quantity { get; set; }
        public TypeVoucherDiscount DiscountType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UtilizationDate { get; set; }
        public DateTime ValidateDate { get; set; }
        public bool Active { get; set; }
        public bool Used { get; set; }

        public bool IsValidToUse()
        {
            return new VoucherSpec.VoucherActiveSpecification()
                .And(new VoucherSpec.VoucherQuantitySpecification())
                .And(new VoucherSpec.VoucherDateSpecification())
                .IsSatisfiedBy(this);
        }
    }
}

using System;
using System.Linq.Expressions;
using NetDevPack.Specification;

namespace NSE.Order.Domain.Vouchers.Specs
{
    public class VoucherSpec
    {
        public class VoucherDateSpecification : Specification<Voucher>
        {
            public override Expression<Func<Voucher, bool>> ToExpression()
            {
                return voucher => voucher.ValidateDate >= DateTime.Now;
            }
        }

        public class VoucherQuantitySpecification : Specification<Voucher>
        {
            public override Expression<Func<Voucher, bool>> ToExpression()
            {
                return voucher => voucher.Quantity > 0;
            }
        }

        public class VoucherActiveSpecification : Specification<Voucher>
        {
            public override Expression<Func<Voucher, bool>> ToExpression()
            {
                return voucher => voucher.Active && !voucher.Used;
            }
        }
    }
}

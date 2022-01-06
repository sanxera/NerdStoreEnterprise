using NetDevPack.Specification;

namespace NSE.Order.Domain.Vouchers.Specs
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dateSpec = new VoucherSpec.VoucherDateSpecification();
            var quantitySpec = new VoucherSpec.VoucherQuantitySpecification();
            var activeSpec = new VoucherSpec.VoucherActiveSpecification();

            Add("dateSpec", new Rule<Voucher>(dateSpec, "Este voucher está expirado"));
            Add("quantitySpec", new Rule<Voucher>(quantitySpec, "Este voucher já foi utilizado"));
            Add("activeSpec", new Rule<Voucher>(activeSpec, "Este voucher não está mais ativo"));
        }
    }
}
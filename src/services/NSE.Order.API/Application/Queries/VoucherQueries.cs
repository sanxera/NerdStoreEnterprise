using System.Threading.Tasks;
using NSE.Order.API.Application.DTO;
using NSE.Order.Domain.Vouchers.Interfaces;

namespace NSE.Order.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> FindVoucherByCode(string code);
    }

    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> FindVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.FindVoucherByCode(code);

            if (voucher == null) return null;

            if (!voucher.IsValidToUse()) return null;

            return new VoucherDTO()
            {
                Code = voucher.Code,
                DiscountType = (int)voucher.DiscountType,
                Percent = voucher.Percent,
                ValueDiscount = voucher.ValueDiscount
            };
        }
    }
}
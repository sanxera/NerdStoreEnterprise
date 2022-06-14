using System.Threading.Tasks;

namespace NSE.Order.Domain.Vouchers.Interfaces
{
    public interface IVoucherRepository
    {
        Task<Voucher> FindVoucherByCode(string code);
    }
}

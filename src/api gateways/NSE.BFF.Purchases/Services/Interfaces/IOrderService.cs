using System.Threading.Tasks;
using NSE.BFF.Purchases.Models;

namespace NSE.BFF.Purchases.Services.Interfaces
{
    public interface IOrderService
    {
        Task<VoucherDTO> FindVoucherByCode(string code);
    }
}

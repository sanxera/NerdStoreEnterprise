using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Order.API.Application.DTO;
using NSE.Order.API.Application.Queries;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Order.API.Controllers
{
    public class VoucherController : MainController
    {
        private readonly VoucherQueries _voucherQueries;

        public VoucherController(VoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("voucher/{code}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FindByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return NotFound();

            var voucher = await _voucherQueries.FindVoucherByCode(code);

            return voucher == null ? NotFound() : CustomResponse(voucher);
        }
    }
}

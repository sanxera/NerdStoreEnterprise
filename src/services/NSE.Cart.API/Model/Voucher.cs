using NSE.Cart.API.Model.Enums;

namespace NSE.Cart.API.Model
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal? Percent { get; set; }
        public decimal? ValueDiscount { get; set; }
        public TypeVoucherDiscount DiscountType { get; set; }
    }
}

namespace NSE.BFF.Purchases.Models
{
    public class VoucherDTO
    {
        public string Code { get; set; }
        public decimal? Percent { get; set; }
        public decimal? ValueDiscount { get; set; }
        public int DiscountType { get; set; }
    }
}

namespace FahasaStoreClientApp.Models
{
    public class Voucher
    {
        public Voucher()
        {

        }
        public Voucher(int id, string name, string code, string description, double discountPercent,
            DateTime expirationDate, Decimal minOrderAmount, decimal maxDiscountAmount, int usageLimit)
        {
            VoucherId = id;
            Name = name;
            Code = code;
            Description = description;
            DiscountPercent = discountPercent;
            ExpirationDate = expirationDate;
            MinOrderAmount = minOrderAmount;
            MaxDiscountAmount = maxDiscountAmount;
            UsageLimit = usageLimit;
        }
        public int VoucherId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal MaxDiscountAmount { get; set; }
        public int UsageLimit { get; set; }
    }
}

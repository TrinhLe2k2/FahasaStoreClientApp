namespace FahasaStoreClientApp.Models
{
    public class Order
    {
        public Order(int orderId, DateTime orderDate, string? receiver, decimal totalAmount, string? status, ICollection<OrderDetail>? ordersDetail, Voucher? voucher, decimal? reducedAmountByVoucher)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            Receiver = receiver;
            TotalAmount = totalAmount;
            Status = status;
            OrdersDetail = ordersDetail;
            Voucher = voucher;
            ReducedAmountByVoucher = reducedAmountByVoucher;
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Receiver { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public ICollection<OrderDetail>? OrdersDetail { get; set; }
        public Voucher? Voucher { get; set; }
        public decimal? ReducedAmountByVoucher { get; set; }
    }
}

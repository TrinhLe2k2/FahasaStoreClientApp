namespace FahasaStoreClientApp.Models
{
    public class OrderDetail
    {
        public OrderDetail(int orderDetailId, string productName, string urlImgProduct, decimal originalPrice, decimal currentPrice, decimal quantity, decimal price)
        {
            OrderDetailId = orderDetailId;
            ProductName = productName;
            UrlImgProduct = urlImgProduct;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
            Quantity = quantity;
            Price = price;
        }

        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }
        public string UrlImgProduct { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

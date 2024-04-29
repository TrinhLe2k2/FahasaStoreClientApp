namespace FahasaStoreClientApp.Models.ViewModels
{
    public class FlashSaleBookVM
    {
        public FlashSaleBookVM()
        {
        }

        public FlashSaleBookVM(int flashSaleBookId, FlashSaleVM flashSale, BookVM book, int discountPercentage, int quantity, int sold)
        {
            FlashSaleBookId = flashSaleBookId;
            FlashSale = flashSale;
            Book = book;
            DiscountPercentage = discountPercentage;
            Quantity = quantity;
            Sold = sold;
        }

        public int FlashSaleBookId { get; set; }
        public FlashSaleVM? FlashSale { get; set; }
        public BookVM? Book { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
    }
}

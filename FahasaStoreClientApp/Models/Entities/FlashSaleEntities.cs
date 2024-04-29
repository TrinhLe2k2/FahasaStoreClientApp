using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.Models.Entities
{
    public class FlashSaleEntities
    {
        public FlashSaleEntities()
        {

        }

        public FlashSaleEntities(FlashSaleVM? flashSale, ICollection<FlashSaleBookVM>? flashSaleBook)
        {
            FlashSale = flashSale;
            FlashSaleBook = flashSaleBook;
        }

        public FlashSaleVM? FlashSale { get; set; }
        public ICollection<FlashSaleBookVM>? FlashSaleBook { get; set; }
    }
}

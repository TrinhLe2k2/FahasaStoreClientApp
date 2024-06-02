using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class FlashSaleData
    {
        public List<FlashSaleVM> FlashSales { get; }

        public FlashSaleData()
        {
            FlashSales = [];
            for (int i = 1; i < 13; i++)
            {
                FlashSaleVM FlashSale = new(i, new DateTime(2024, 4, 28), new DateTime(2024, 11, 11));
                FlashSales.Add(FlashSale);
            }
        }

        public IEnumerable<FlashSaleVM> ListFlashSales()
        {
            return FlashSales;
        }

        public FlashSaleVM? FlashSale(int id)
        {
            var FlashSale = FlashSales.SingleOrDefault(b => b.FlashSaleId == id);
            return FlashSale;
        }
    }
}

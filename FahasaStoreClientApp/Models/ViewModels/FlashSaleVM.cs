namespace FahasaStoreClientApp.Models.ViewModels
{
    public class FlashSaleVM
    {
        public FlashSaleVM(int flashSaleId, DateTime startDate, DateTime endDate)
        {
            FlashSaleId = flashSaleId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int FlashSaleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

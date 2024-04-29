using FahasaStoreClientApp.Models.Entities;
using FahasaStoreClientApp.Models.ViewModels;
using System.Security.Policy;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class FlashSaleBookData
    {
        public List<FlashSaleBookVM> FlashSaleBooks { get; }

        public FlashSaleBookData()
        {
            FlashSaleBooks = [];
            var FlashSale = new FlashSaleData().FlashSale(1);
            var books = new BookData();
            var Authors = new AuthorData();
            var CoverTypes = new BookCoverTypeData();
            var Dimensions = new DimensionData();
            var Publishers = new PublisherData();
            var Subcategorys = new SubcategoryData();
            var Suppliers = new SupplierData();

            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/MenuImage/lf" + i + ".png"; 
                FlashSaleBookVM FlashSaleBook = new(i, FlashSale ?? new (1, new DateTime(2024, 4, 27), new DateTime(2024, 5, 5)), 
                    books?.Book(i) ?? new BookVM
                            (
                                i, "Tên sách " + i, "Mô tả sách " + i, 100000, 120000, 20, urlImage, 300, 200, 4, 58,
                                Authors?.Author(i) ?? new AuthorVM(i, "New tác giả " + i),
                                CoverTypes?.BookCoverType(i) ?? new BookCoverTypeVM(i, "Bìa " + i),
                                Dimensions?.Dimension(i) ?? new DimensionVM(i, 18, 13, 3, "cm"),
                                Publishers?.Publisher(i) ?? new PublisherVM(i, "Nhà xuất bản " + i, i + " Trần Hưng Đạo", "033788058" + i, i + "nhaxuatban@gmail.com"),
                                Subcategorys?.Subcategory(i) ?? new SubcategoryVM(i, "Thể Loại Non-" + i, urlImage),
                                Suppliers?.Supplier(i) ?? new SupplierVM(i, "Nhà cung cấp " + i, i + " Điện biên phủ", "033887015" + i, i + "nhacungcap@gmail.com")
                            ), 
                    
                    28, 200, 20); 
                
                FlashSaleBooks.Add(FlashSaleBook);
            }
        }
        public IEnumerable<FlashSaleBookVM>
                ListFlashSaleBooks()
        {
            return FlashSaleBooks;
        }

        public FlashSaleBookVM? FlashSaleBook(int id)
        {
            var FlashSaleBook = FlashSaleBooks.SingleOrDefault(b => b.FlashSaleBookId == id);
            return FlashSaleBook;
        }

        public FlashSaleEntities? FlashSaleToday(int? number)
        {
            var today = DateTime.Today;

            var FlashSaleToday = FlashSaleBooks?.Where(b => b.FlashSale?.StartDate <= today && today <= b.FlashSale.EndDate);

            var formatData = new FlashSaleEntities
            {
                FlashSale = FlashSaleToday?.FirstOrDefault()?.FlashSale,
                FlashSaleBook = number == null ? FlashSaleToday?.ToList() : FlashSaleToday?.Take((int)number).ToList()
            };

            return formatData;
        }
    }
}

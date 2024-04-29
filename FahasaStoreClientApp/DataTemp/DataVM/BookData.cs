using FahasaStoreClientApp.Models.ViewModels;
using System.Security.Policy;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class BookData
    {
        public List<BookVM> Books { get; }

        public BookData()
        {
            Books = [];
            var Authors = new AuthorData();
            var CoverTypes = new BookCoverTypeData();
            var Dimensions = new DimensionData();
            var Publishers = new PublisherData();
            var Subcategorys = new SubcategoryData();
            var Suppliers = new SupplierData();

            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/BookImage/fs" + i + ".jpg";

                BookVM book = new
                (
                    i, "Tên sách " + i, "Mô tả sách " + i, 100000, 120000, 20, urlImage, 300, 200, 4, 16,
                    Authors?.Author(i) ?? new AuthorVM(i, "New tác giả " + i),
                    CoverTypes?.BookCoverType(i) ?? new BookCoverTypeVM(i, "Bìa " + i),
                    Dimensions?.Dimension(i) ?? new DimensionVM(i, 18, 13, 3, "cm"),
                    Publishers?.Publisher(i) ?? new PublisherVM(i, "Nhà xuất bản " + i, i + " Trần Hưng Đạo", "033788058" + i, i + "nhaxuatban@gmail.com"),
                    Subcategorys?.Subcategory(i) ?? new SubcategoryVM(i, "Thể Loại Non-" + i, urlImage),
                    Suppliers?.Supplier(i) ?? new SupplierVM(i, "Nhà cung cấp " + i, i + " Điện biên phủ", "033887015" + i, i + "nhacungcap@gmail.com")
                );

                Books.Add(book);
            }
        }

        public IEnumerable<BookVM> ListBooks()
        {
            return Books;
        }

        public IEnumerable<BookVM> ListLimitedBooks(int number)
        {
            return Books.Take(number).ToList();
        }

        public BookVM? Book(int id)
        {
            var Book = Books.SingleOrDefault(b => b.BookId == id);
            return Book;
        }
    }
}

using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class BookCoverTypeData
    {
        public List<BookCoverTypeVM> BookCoverTypes { get; }

        public BookCoverTypeData()
        {
            BookCoverTypes = [];
            for (int i = 1; i < 13; i++)
            {
                BookCoverTypeVM bookCoverType = new(i, "Bìa "+i);
                BookCoverTypes.Add(bookCoverType);
            }
        }

        public IEnumerable<BookCoverTypeVM> ListBookCoverTypes()
        {
            return BookCoverTypes;
        }

        public BookCoverTypeVM? BookCoverType(int id)
        {
            var BookCoverType = BookCoverTypes.SingleOrDefault(b => b.CoverTypeId == id);
            return BookCoverType;
        }
    }
}

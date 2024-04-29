using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class AuthorData
    {
        public List<AuthorVM> Authors { get; }

        public AuthorData()
        {
            Authors = [];
            for (int i = 1; i < 13; i++)
            {
                AuthorVM author = new(i, "Author Name " + i);
                Authors.Add(author);
            }
        }

        public IEnumerable<AuthorVM> ListAuthors()
        {
            return Authors;
        }

        public AuthorVM? Author(int id)
        {
            var Author = Authors.SingleOrDefault(e => e.AuthorId == id);
            return Author;
        }
    }
}

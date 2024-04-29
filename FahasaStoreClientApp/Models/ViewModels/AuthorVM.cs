namespace FahasaStoreClientApp.Models.ViewModels
{
    public class AuthorVM
    {
        public AuthorVM(int authorId, string name)
        {
            AuthorId = authorId;
            Name = name;
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}

namespace FahasaStoreClientApp.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM(int categoryId, string name, string imageUrl)
        {
            CategoryId = categoryId;
            Name = name;
            ImageUrl = imageUrl;
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}

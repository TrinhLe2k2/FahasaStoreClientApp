namespace FahasaStoreClientApp.Models.ViewModels
{
    public class SubcategoryVM
    {
        public SubcategoryVM(int subcategoryId, string name, string imageUrl)
        {
            SubcategoryId = subcategoryId;
            Name = name;
            ImageUrl = imageUrl;
        }

        public int SubcategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; }
    }
}

namespace FahasaStoreClientApp.Models.BaseModel
{
    public class CategoryBase
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}

namespace FahasaStoreClientApp.Models.BaseModel
{
    public class SubcategoryBase
    {
        public int SubcategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}

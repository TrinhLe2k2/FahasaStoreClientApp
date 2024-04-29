using System.Security.Policy;

namespace FahasaStoreClientApp.Models.ViewModels
{
    public class BookVM
    {
        public BookVM(int bookId, string name, string description, decimal originalPrice, decimal currentPrice, double discountPercentage, string? imageUrl, double? weight, int? pageCount, int rate, int sold, AuthorVM author, BookCoverTypeVM? coverType, DimensionVM? dimension, PublisherVM publisher, SubcategoryVM subcategory, SupplierVM supplier)
        {
            BookId = bookId;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
            DiscountPercentage = discountPercentage;
            ImageUrl = imageUrl;
            Weight = weight;
            PageCount = pageCount;
            Rate = rate;
            Sold = sold;
            Author = author;
            CoverType = coverType;
            Dimension = dimension;
            Publisher = publisher;
            Subcategory = subcategory;
            Supplier = supplier;
        }

        public int BookId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public string? ImageUrl { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
        public int? Rate { get; set; }
        public int? Sold { get; set; }

        public AuthorVM Author { get; set; }
        public BookCoverTypeVM? CoverType { get; set; }
        public DimensionVM? Dimension { get; set; }
        public PublisherVM Publisher { get; set; }
        public SubcategoryVM Subcategory { get; set; }
        public SupplierVM Supplier { get; set; }
    }
}

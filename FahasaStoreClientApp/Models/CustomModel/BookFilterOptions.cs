using System.ComponentModel;

namespace FahasaStoreClientApp.Models.CustomModels
{
    public class BookFilterOptions
    {
        [DisplayName("Sắp Xếp")]
        public string SortBy { get; set; } = string.Empty;
        [DisplayName("Tìm Kiếm")]
        public string Search { get; set; } = string.Empty;
        [DisplayName("Thể Loại")]
        public int? CategoryId { get; set; }
        [DisplayName("Thể Loại Con")]
        public int? SubcategoryId { get; set; }
        [DisplayName("Tác Giả")]
        public int? AuthorId { get; set; }
        [DisplayName("Đồng Giá Từ")]
        public int? MinPrice { get; set; }
        [DisplayName("Tối Đa")]
        public int? MaxPrice { get; set; }
        [DisplayName("Đối Tác")]
        public int? PartnerId { get; set; }
        [DisplayName("Kiểu Đối Tác")]
        public int? PartnerTypeId { get; set; }
        [DisplayName("Loại Bìa Sách")]
        public int? CoverTypeId { get; set; }
        public bool FlashSale { get; set; }
    }
}

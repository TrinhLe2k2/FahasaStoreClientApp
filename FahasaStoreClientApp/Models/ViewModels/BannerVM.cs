namespace FahasaStoreClientApp.Models.ViewModels
{
    public class BannerVM
    {
        public BannerVM(int bannerId, string imageUrl, string title, string content, DateTime createdAt)
        {
            BannerId = bannerId;
            ImageUrl = imageUrl;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
        }

        public int BannerId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace FahasaStoreClientApp.Models.ViewModels
{
    public class MenuVM
    {
        public MenuVM(int menuId, string imageUrl, string title, string link)
        {
            MenuId = menuId;
            ImageUrl = imageUrl;
            Title = title;
            this.link = link;
        }

        public int MenuId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string link { get; set; }

    }
}

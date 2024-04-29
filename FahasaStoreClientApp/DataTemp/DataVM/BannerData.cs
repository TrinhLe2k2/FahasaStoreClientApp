using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class BannerData
    {
        public List<BannerVM> Banners { get; }

        public BannerData()
        {
            Banners = [];
            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/BannerImage/" + i + ".jpg";
                BannerVM banner = new(i, urlImage, "title" + i, "content" + i, new DateTime());
                Banners.Add(banner);
            }
        }

        public IEnumerable<BannerVM> ListBanners()
        {
            return Banners;
        }

        public BannerVM? Banner(int id)
        {
            var banner = Banners.SingleOrDefault(b => b.BannerId == id);
            return banner;
        }
    }
}

using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class MenuData
    {
        public List<MenuVM> menus { get; }

        public MenuData()
        {
            menus = [];
            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/MenuImage/lf" + i + ".png";
                MenuVM menu = new(i, urlImage, "Menu" + i, "#");
                menus.Add(menu);
            }
        }

        public IEnumerable<MenuVM> ListMenus()
        {
            return menus;
        }
    }
}

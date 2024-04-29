using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class SubcategoryData
    {
        public List<SubcategoryVM> Subcategorys { get; }

        public SubcategoryData()
        {
            Subcategorys = [];
            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/MenuImage/" + i + ".jpg";
                SubcategoryVM Subcategory = new(i, "Thể Loại Non-"+i, urlImage);
                Subcategorys.Add(Subcategory);
            }
        }

        public IEnumerable<SubcategoryVM> ListSubcategorys()
        {
            return Subcategorys;
        }

        public SubcategoryVM? Subcategory(int id)
        {
            var Subcategory = Subcategorys.SingleOrDefault(b => b.SubcategoryId == id);
            return Subcategory;
        }
    }
}

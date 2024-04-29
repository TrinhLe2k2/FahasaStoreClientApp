using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class CategoryData
    {
        public List<CategoryVM> Categorys { get; }

        public CategoryData()
        {
            Categorys = [];
            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/MenuImage/" + i + ".jpg";
                CategoryVM Category = new(i, "Thể Loại "+i, urlImage);
                Categorys.Add(Category);
            }
        }

        public IEnumerable<CategoryVM> ListCategorys()
        {
            return Categorys;
        }

        public CategoryVM? Category(int id)
        {
            var Category = Categorys.SingleOrDefault(b => b.CategoryId == id);
            return Category;
        }
    }
}

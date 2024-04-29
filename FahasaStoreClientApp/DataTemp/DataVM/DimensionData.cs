using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class DimensionData
    {
        public List<DimensionVM> Dimensions { get; }

        public DimensionData()
        {
            Dimensions = [];
            for (int i = 1; i < 13; i++)
            {
                DimensionVM Dimension = new(i, 18, 13, 3, "cm");
                Dimensions.Add(Dimension);
            }
        }

        public IEnumerable<DimensionVM> ListDimensions()
        {
            return Dimensions;
        }

        public DimensionVM? Dimension(int id)
        {
            var Dimension = Dimensions.SingleOrDefault(b => b.DimensionId == id);
            return Dimension;
        }
    }
}

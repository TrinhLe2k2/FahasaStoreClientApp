using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class SupplierData
    {
        public List<SupplierVM> Suppliers { get; }

        public SupplierData()
        {
            Suppliers = [];
            for (int i = 1; i < 13; i++)
            {
                string urlImage = "/image/MenuImage/" + i + ".jpg";
                SupplierVM Supplier = new(i, "Nhà cung cấp "+i, i+" Điện biên phủ", "033887015"+i, i+"nhacungcap@gmail.com");
                Suppliers.Add(Supplier);
            }
        }

        public IEnumerable<SupplierVM> ListSuppliers()
        {
            return Suppliers;
        }

        public SupplierVM? Supplier(int id)
        {
            var Supplier = Suppliers.SingleOrDefault(b => b.SupplierId == id);
            return Supplier;
        }
    }
}

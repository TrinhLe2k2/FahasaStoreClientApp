namespace FahasaStoreClientApp.Models.ViewModels
{
    public class SupplierVM
    {
        public SupplierVM(int supplierId, string name, string address, string phone, string email)
        {
            SupplierId = supplierId;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }

        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

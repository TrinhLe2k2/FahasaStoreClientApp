namespace FahasaStoreClientApp.Models.ViewModels
{
    public class PublisherVM
    {
        public PublisherVM(int publisherId, string name, string address, string phone, string email)
        {
            PublisherId = publisherId;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }

        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

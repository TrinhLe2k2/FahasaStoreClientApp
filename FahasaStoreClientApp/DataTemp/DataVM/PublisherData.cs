using FahasaStoreClientApp.Models.ViewModels;

namespace FahasaStoreClientApp.DataTemp.DataVM
{
    public class PublisherData
    {
        public List<PublisherVM> Publishers { get; }

        public PublisherData()
        {
            Publishers = [];
            for (int i = 1; i < 13; i++)
            {
                PublisherVM Publisher = new(i, "Nhà xuất bản "+i, i+" Trần Hưng Đạo", "033788058"+i, i+"nhaxuatban@gmail.com");
                Publishers.Add(Publisher);
            }
        }

        public IEnumerable<PublisherVM> ListPublishers()
        {
            return Publishers;
        }

        public PublisherVM? Publisher(int id)
        {
            var Publisher = Publishers.SingleOrDefault(b => b.PublisherId == id);
            return Publisher;
        }
    }
}

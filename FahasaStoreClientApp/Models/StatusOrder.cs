using System.ComponentModel.DataAnnotations;

namespace FahasaStoreClientApp.Models
{
    public class StatusOrder
    {
        public StatusOrder(int statusId, string name)
        {
            StatusId = statusId;
            Name = name;
        }
        [Display(Name="Trạng thái đơn hàng")]
        public int StatusId { get; set; }
        public string Name { get; set; }
    }
}

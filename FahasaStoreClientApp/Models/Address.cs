using System.ComponentModel.DataAnnotations;

namespace FahasaStoreClientApp.Models
{
    public class Address
    {
        [Display(Name = "Tên người nhận")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ReceiverName { get; set; } = null!;
        [Display(Name = "Tỉnh / Thành Phố")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Province { get; set; } = null!;
        [Display(Name = "Quận / Huyện")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string District { get; set; } = null!;
        [Display(Name = "Phường / Xã")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Ward { get; set; } = null!;
        [Display(Name = "Địa chỉ cụ thể")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Detail { get; set; } = null!;
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^(\+?84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = null!;
        [Display(Name = "Địa chỉ mặc định")]
        public bool? DefaultAddress { get; set; }
    }

    public class Province
    {
        public int? PROVINCE_ID { get; set; }
        public string? PROVINCE_CODE { get; set; }
        public string? PROVINCE_NAME { get; set; }
    }

    public class District
    {
        public int? DISTRICT_ID { get; set; }
        public int? PROVINCE_ID { get; set; }
        public string? DISTRICT_VALUE { get; set; }
        public string? DISTRICT_NAME { get; set; }
    }

    public class Ward
    {
        public int? WARDS_ID { get; set; }
        public int? DISTRICT_ID { get; set; }
        public string? WARDS_NAME { get; set; }
    }
}

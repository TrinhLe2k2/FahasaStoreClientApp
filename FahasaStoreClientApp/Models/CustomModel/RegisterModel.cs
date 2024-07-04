using System.ComponentModel.DataAnnotations;

namespace FahasaStoreClientApp.Models.CustomModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập email!"), EmailAddress]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Mật khẩu xác nhận chưa chính xác, vui lòng nhập lại!"), Compare("Password")]
        [Display(Name = "Xác nhận lại mật khẩu")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

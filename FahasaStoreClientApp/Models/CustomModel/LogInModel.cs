using System.ComponentModel.DataAnnotations;

namespace FahasaStoreClientApp.Models.CustomModel
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email"), EmailAddress(ErrorMessage = "Vui lòng nhập email hợp lệ")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

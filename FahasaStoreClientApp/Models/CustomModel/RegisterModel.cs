using System.ComponentModel.DataAnnotations;

namespace FahasaStoreClientApp.Models.CustomModel
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required, Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

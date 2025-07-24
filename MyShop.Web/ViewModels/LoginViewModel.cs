using System.ComponentModel.DataAnnotations;

namespace MyShop.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Angemeldet bleiben")]
        public bool RememberMe { get; set; }
    }
}
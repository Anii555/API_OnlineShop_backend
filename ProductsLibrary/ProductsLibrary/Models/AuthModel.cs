using System.ComponentModel.DataAnnotations;

namespace ProductsLibrary.Models
{
    public class AuthModel
    {
        [Required(ErrorMessage = "Придумайте логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Придумайте пароль")]
        public string Password { get; set; }
    }
}

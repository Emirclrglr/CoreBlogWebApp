using System.ComponentModel.DataAnnotations;

namespace CoreBlogWebApp.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı adınızı giriniz.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi giriniz.")]

        public string Password { get; set; }
    }
}

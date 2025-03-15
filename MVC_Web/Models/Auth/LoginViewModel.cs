using System.ComponentModel.DataAnnotations;

namespace MVC_Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public LoginViewModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

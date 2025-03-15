using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
        [MaxLength(13)]
        [RegularExpression("^\\+?\\d{9,15}$")]
        public string Phone { set; get; }
        [MaxLength(255)]
        [EmailAddress]
        public string Email { set; get; }
        [MaxLength(255)]
        public string Address { set; get; }
        [MaxLength(255)]
        public string City { set; get; }
        [RegularExpression("^\\d{3} ?\\d{2}$")]
        public string PSC { set; get; }
        [MaxLength(50)]
        public string Role { set; get; }

        public RegisterViewModel(string firstName, string lastName, string username, string password, string phone, string email, string address, string city, string pSC, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            PSC = pSC;
            Role = role;
        }

        public RegisterViewModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            PSC = string.Empty;
            Role = "user";
        }
    }
}

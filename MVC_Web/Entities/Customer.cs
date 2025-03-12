using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbZakaznici")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("jmeno")]
        public string FirstName { get; set; }
        [Column("prijmeni")]
        public string LastName { get; set; }
        [Column("heslo")]
        public string Password { get; set; }
        [Column("telefon")]
        public string Phone { set; get; }
        [Column("email")]
        public string Email { set; get; }
        [Column("ulice")]
        public string Address{ set; get; }
        [Column("mesto")]
        public string City { set; get; }
        [Column("psc")]
        public string PSC { set; get; }
        [Column("role")]
        public string Role { set; get; }

        public Customer(int id, string firstName, string lastName, string password, string phone, string email, string address, string city, string pSC, string role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            PSC = pSC;
            Role = role;
        }

        public Customer()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Password = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            PSC = string.Empty;
            Role = string.Empty;
        }
    }
}

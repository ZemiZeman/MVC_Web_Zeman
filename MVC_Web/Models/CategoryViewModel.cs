using Org.BouncyCastle.Asn1.Cmp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(100,ErrorMessage = "Maximálně 100 znaků")]
        [Required(ErrorMessage = "Pole nemůže být prázdné!")]
        [RegularExpression("^[A-Z][a-zA-ZÀ-ž,. ]*$", ErrorMessage ="Kategorie musí začínat velkým písmenem!")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Maximálně 200 znaků")]
        [Required(ErrorMessage = "Pole nemůže být prázdné!")]
        [RegularExpression("^[A-Z][a-zA-ZÀ-ž,. ]*$", ErrorMessage = "Kategorie musí začínat velkým písmenem!")]
        public string Description { get; set; }

        public CategoryViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
        }
        public CategoryViewModel(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}

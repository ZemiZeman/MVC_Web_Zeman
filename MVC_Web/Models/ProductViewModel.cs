using Microsoft.AspNetCore.Antiforgery;
using MVC_Web.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVC_Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression("^[A-Z][a-zA-ZÀ-ž,. ]*$", ErrorMessage = "Produkt musí začínat velkým písmenem!")]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        [MaxLength(2048)]
        [RegularExpression("^[A-Z][a-zA-ZÀ-ž,. ]*$", ErrorMessage = "Popis musí začínat velkým písmenem!")]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ManufacturerId { get; set; }

        public virtual CategoryViewModel Category { get; set; }
        public virtual ManufacturerViewModel Manufacturer { get; set; }

        public IFormFile? ImageFile { get; set; }
        public List<ReviewViewModel>? Reviews { get; set; }

        public ProductViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Price = 0;
            Description = string.Empty;
            CategoryId = 0;
            ManufacturerId = 0;
            Category = new CategoryViewModel();
            Manufacturer = new ManufacturerViewModel();
            ImageFile = null;
            Reviews = new List<ReviewViewModel>();
        }
        public ProductViewModel(int id, string name, float price, string description, int categoryId, int manufacturerId, CategoryViewModel category, ManufacturerViewModel manufacturer)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            CategoryId = categoryId;
            ManufacturerId = manufacturerId;
            Category = category;
            Manufacturer = manufacturer;
        }
    }
}

using MVC_Web.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public CategoryViewModel Category { get; set; }
        public ManufacturerViewModel Manufacturer { get; set; }

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

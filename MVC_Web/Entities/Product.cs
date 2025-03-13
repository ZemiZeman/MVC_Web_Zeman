using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbVyrobky")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nazev")]
        public string Name { get; set; }
        [Column("cena")]
        public float Price { get; set; }
        [Column("popis")]
        public string Description { get; set; }
        [Column("kategorieId")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Column("vyrobceId")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public Product()
        {
            Id = 0;
            Name = string.Empty;
            Price = 0;
            Description = string.Empty;
            CategoryId = 0;
            ManufacturerId = 0;
            Category = new Category();
            Manufacturer = new Manufacturer();
        }
        public Product(int id, string name, float price, string description, int categoryId, int manufacturerId, Category category, Manufacturer manufacturer)
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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbRecenze")]
    public class Review
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("datum")]
        public DateTime Date { get; set; }
        [Column("hodnota")]
        public int Value { get; set; }
        [Column("popis")]
        public string Description { get; set; }
        [Column("vyrobekId")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [NotMapped]
        public virtual Product Product { get; set; }

        public Review()
        {
            Id = 0;
            Date = DateTime.Now;
            Value = 0;
            Description = string.Empty;
            ProductId = 0;
            Product = new Product();

        }
        public Review(int id, DateTime date, int value, string description, int productId, Product product)
        {
            Id = id;
            Date = date;
            Value = value;
            Description = description;
            ProductId = productId;
            Product = product;
        }
    }
}

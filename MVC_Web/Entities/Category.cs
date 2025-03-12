using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbKategorie")] 
    public class Category
    {
        [Key]
        [Column("cislo_kategorie")]
        public int Id { get; set; }
        [Column("nazev_kategorie")]
        public string Name { get; set; }
        [Column("popis_kategorie")]
        public string Description { get; set; }

        public Category()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
        }
        public Category(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}

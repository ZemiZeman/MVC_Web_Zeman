using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbVyrobci")]
    public class Manufacturer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nazev")]
        public string Name { get; set; }
        [Column("zeme_puvodu")]
        public string CounryOrigin { get; set; }

        public Manufacturer(int id, string name, string counryOrigin)
        {
            Id = id;
            Name = name;
            CounryOrigin = counryOrigin;
        }

        public Manufacturer()
        {
            Id = 0;
            Name = string.Empty;
            CounryOrigin = string.Empty;
        }

        public Manufacturer(string name, string counryOrigin)
        {
            Id = 0;
            Name = name;
            CounryOrigin = counryOrigin;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbDoruceni")]
    public class Delivery
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("popis")]
        public string Name { get; set; }

        public Delivery(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Delivery()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}

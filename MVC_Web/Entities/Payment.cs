using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Entities
{
    [Table("tbMoznostiPlatby")]
    public class Payment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("popis")]
        public string Name { get; set; }

        public Payment(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Payment()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DeliveryViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public DeliveryViewModel()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}

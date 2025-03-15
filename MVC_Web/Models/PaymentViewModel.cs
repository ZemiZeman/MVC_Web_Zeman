using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PaymentViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public PaymentViewModel()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}

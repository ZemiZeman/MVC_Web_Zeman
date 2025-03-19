using MVC_Web.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Range(1,5)]
        [Required]
        public int Value { get; set; }
        [Required(ErrorMessage = "Nemůže být prázdný!")]
        [MaxLength(2048,ErrorMessage = "Maximálně 2048 znaků!")]
        public string Description { get; set; }
        public int ProductId { get; set; }

        public ReviewViewModel()
        {
            Id = 0;
            Date = DateTime.Now;
            Value = 0;
            Description = string.Empty;
            ProductId = 0;

        }
        public ReviewViewModel(int id, DateTime date, int value, string description, int productId)
        {
            Id = id;
            Date = date;
            Value = value;
            Description = description;
            ProductId = productId;
        }
    }
}

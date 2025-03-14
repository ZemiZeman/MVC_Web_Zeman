using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryViewModel()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
        }
        public CategoryViewModel(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Web.Models
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CounryOrigin { get; set; }

        public ManufacturerViewModel(int id, string name, string counryOrigin)
        {
            Id = id;
            Name = name;
            CounryOrigin = counryOrigin;
        }

        public ManufacturerViewModel()
        {
            Id = 0;
            Name = string.Empty;
            CounryOrigin = string.Empty;
        }

        public ManufacturerViewModel(string name, string counryOrigin)
        {
            Id = 0;
            Name = name;
            CounryOrigin = counryOrigin;
        }
    }
}

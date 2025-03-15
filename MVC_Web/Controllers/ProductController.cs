using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;

namespace MVC_Web.Controllers
{
    public class ProductController : BaseController
    {
        private DatabaseContext  _dbContext;

        public ProductController(DatabaseContext dbDontext)
        {
            _dbContext = dbDontext;
        }

        [HttpGet]
        public IActionResult Index([FromRoute]int id)
        {
            Product p = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            ProductViewModel product = new ProductViewModel(p.Id, p.Name, p.Price, p.Description, p.CategoryId, p.ManufacturerId,
                                                            new CategoryViewModel(p.Category.Id, p.Category.Name, p.Category.Description),
                                                            new ManufacturerViewModel(p.Manufacturer.Name, p.Manufacturer.CounryOrigin));

            return View(product);
        }
    }
}

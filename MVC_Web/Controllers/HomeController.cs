using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;
using System.Diagnostics;

namespace MVC_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string? category)
        {

            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m=> new ManufacturerViewModel(m.Id,m.Name,m.CounryOrigin)).ToList();
            List<CategoryViewModel> categories = _dbContext.Categories.Select(c=> new CategoryViewModel(c.Id,c.Name,c.Description)).ToList();

            ViewBag.Categories = categories;

            List<Product> entityProducts = _dbContext.Products.ToList();
            
            List<ProductViewModel> products = entityProducts
                                                .Select(p=> new ProductViewModel(p.Id,p.Name,p.Price,p.Description,p.CategoryId,p.ManufacturerId,
                                                categories.FirstOrDefault(c=>c.Id==p.CategoryId)!,manufacturers.FirstOrDefault(m=> m.Id ==p.ManufacturerId)!)).ToList();
            

            if (category != null)
            {
               List<ProductViewModel> productWithCategory = products.Where(p => p.Category.Name == category).ToList();
               return View(productWithCategory);
            }


            return View(products);
        }


       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MVC_Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private DatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string? category,string? searchFilter,int? manufacturerId,float? minPrice,float? maxPrice,bool? redirectedFromProduct)
        {

            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m=> new ManufacturerViewModel(m.Id,m.Name,m.CounryOrigin)).ToList();
            List<CategoryViewModel> categories = _dbContext.Categories.Select(c=> new CategoryViewModel(c.Id,c.Name,c.Description)).ToList();

            ViewBag.Categories = categories;

            List<Product> entityProducts = _dbContext.Products.ToList();
            
            List<ProductViewModel> products = entityProducts
                                                .Select(p=> new ProductViewModel(p.Id,p.Name,p.Price,p.Description,p.CategoryId,p.ManufacturerId,
                                                categories.FirstOrDefault(c=>c.Id==p.CategoryId)!,manufacturers.FirstOrDefault(m=> m.Id ==p.ManufacturerId)!)).ToList();

            if (redirectedFromProduct != null)
            {
                category = HttpContext.Session.GetString("category");
                searchFilter = HttpContext.Session.GetString("searchFilter");
                if(HttpContext.Session.GetString("manufacturerId") != null)
                    manufacturerId = int.Parse(HttpContext.Session.GetString("manufacturerId")!);
                if(HttpContext.Session.GetString("minPrice") != null)
                    minPrice = float.Parse(HttpContext.Session.GetString("minPrice")!);
                if (HttpContext.Session.GetString("maxPrice") != null)
                    maxPrice = float.Parse(HttpContext.Session.GetString("maxPrice")!);
            }
            else
            {
                if(category != null)
                    HttpContext.Session.SetString("category", category!);
                else
                    HttpContext.Session.Remove("category");
                
                if (searchFilter != null)
                    HttpContext.Session.SetString("searchFilter", searchFilter!);
                else
                    HttpContext.Session.Remove("searchFilter");
                
                if (manufacturerId != null)    
                    HttpContext.Session.SetString("manufacturerId", manufacturerId.ToString()!);
                else
                    HttpContext.Session.Remove("manufacturerId");

                if (minPrice != null)
                    HttpContext.Session.SetString("minPrice", minPrice.ToString()!);
                else
                    HttpContext.Session.Remove("minPrice");

                if (maxPrice != null)
                    HttpContext.Session.SetString("maxPrice", maxPrice.ToString()!);
                else
                    HttpContext.Session.Remove("maxPrice");
            }
            
                                      

            if (category != null)
            {
                products.RemoveAll(p => p.Category.Name != category);

            }

            if(searchFilter != null)
            {
                products.RemoveAll(p => !Regex.IsMatch(p.Name, searchFilter, RegexOptions.IgnoreCase) && !Regex.IsMatch(p.Description,searchFilter,RegexOptions.IgnoreCase));
            }

            if(manufacturerId != null)
            {
                products.RemoveAll(p=> p.ManufacturerId != manufacturerId);
            }

            if (minPrice == null)
            {
                if(products.Count > 0)
                    minPrice = products.OrderBy(p => p.Price).Select(p => p.Price).First();
            }
                

            if (maxPrice == null)
            {
                if(products.Count > 0)
                    maxPrice = products.OrderByDescending(p => p.Price).Select(p => p.Price).First();
            }
                

            products.RemoveAll(p=> p.Price < minPrice || p.Price > maxPrice);

            ViewBag.Manufacturers = manufacturers;

            return View(products);
        }


        public IActionResult RestartFilter()
        {
            HttpContext.Session.Remove("category");
            HttpContext.Session.Remove("searchFilter");
            HttpContext.Session.Remove("manufacturerId");
            HttpContext.Session.Remove("minPrice");
            HttpContext.Session.Remove("maxPrice");

            return RedirectToAction("Index");
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

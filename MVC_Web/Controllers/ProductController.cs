using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MVC_Web.Attributes;
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
            Product? p = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            

            if (p == null)
            {
                return RedirectToAction("Index","Home");
            }

            Category category = _dbContext.Categories.Single(c => c.Id == p.CategoryId);
            Manufacturer manufacturer = _dbContext.Manufacturer.Single(m=>m.Id==p.ManufacturerId);
            List<Review> reviews = _dbContext.Reviews.Where(r=>r.ProductId==id).ToList();

            ProductViewModel product = new ProductViewModel(p.Id, p.Name, p.Price, p.Description, p.CategoryId, p.ManufacturerId,
                                                            new CategoryViewModel(category.Id, category.Name, category.Description),
                                                            new ManufacturerViewModel(manufacturer.Name, manufacturer.CounryOrigin));

            List<ReviewViewModel> reviewViewModels = reviews.Select(r => new ReviewViewModel(r.Id, r.Date, r.Value, r.Description, r.ProductId)).ToList();
            product.Reviews = reviewViewModels;

            return View(product);
        }

        [RequireRole("admin")]
        public IActionResult List()
        {
            List<Product> productsObj = _dbContext.Products.ToList();

            List<CategoryViewModel> categories = _dbContext.Categories.Select(c=> new CategoryViewModel(c.Id,c.Name,c.Description)).ToList();
            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m=> new ManufacturerViewModel(m.Id,m.Name,m.CounryOrigin)).ToList();

            List<ProductViewModel> products = productsObj.Select(p => new ProductViewModel(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Description,
                    p.CategoryId,
                    p.ManufacturerId,
                    categories.Single(c=>c.Id==p.CategoryId),
                    manufacturers.Single(m=>m.Id==p.ManufacturerId
                ))).ToList();

            return View(products);
        }

        [RequireRole("admin")]
        public IActionResult Add()
        {
            List<CategoryViewModel> categories = _dbContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name, c.Description)).ToList();
            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m => new ManufacturerViewModel(m.Id, m.Name, m.CounryOrigin)).ToList();

            ViewBag.Categories = categories;
            ViewBag.Manufacturers = manufacturers;

            return View(new ProductViewModel());
        }

        [RequireRole("admin")]
        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {

            List<CategoryViewModel> categories = _dbContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name, c.Description)).ToList();
            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m => new ManufacturerViewModel(m.Id, m.Name, m.CounryOrigin)).ToList();

            product.Category = categories.Single(c => c.Id == product.CategoryId);
            product.Manufacturer = manufacturers.Single(m => m.Id == product.ManufacturerId);

            

            Category categoriesObj = _dbContext.Categories.Single(c=> c.Id == product.CategoryId);
            Manufacturer manufacturerObj = _dbContext.Manufacturer.Single(m=> m.Id == product.ManufacturerId);

            //CategoryViewModel category = new CategoryViewModel(categoriesObj.Id,categoriesObj.Name,categoriesObj.Description);
            //ManufacturerViewModel manufacturer = new ManufacturerViewModel(manufacturerObj.Id,manufacturerObj.Name,manufacturerObj.CounryOrigin);

            Product newProduct = new Product(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Description,
                    product.CategoryId,
                    product.ManufacturerId,
                    categoriesObj,
                    manufacturerObj
                );

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            

            return RedirectToAction("List");
        }

        [RequireRole("admin")]
        public IActionResult Edit(int id)
        {
            List<CategoryViewModel> categories = _dbContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name, c.Description)).ToList();
            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m => new ManufacturerViewModel(m.Id, m.Name, m.CounryOrigin)).ToList();

            ViewBag.Categories = categories;
            ViewBag.Manufacturers = manufacturers;

            Product? productObj = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (productObj == null)
            {
                TempData["Message"] = "We cound't find product with this ID";
                TempData["MessageType"] = "danger";

                return RedirectToAction("List");
            }

            ProductViewModel product = new ProductViewModel(
                    productObj.Id,
                    productObj.Name,
                    productObj.Price,
                    productObj.Description,
                    productObj.CategoryId,
                    productObj.ManufacturerId,
                    categories.First(c => c.Id == productObj.CategoryId),
                    manufacturers.First(m => m.Id == productObj.ManufacturerId)
                );

            return View(product);
        }

        [RequireRole("admin")]
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            Product? newProduct= _dbContext.Products.SingleOrDefault(c => c.Id == product.Id);

            if (newProduct == null)
            {
                TempData["Message"] = "We coulnd't find this category!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("List");
            }

            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Description = product.Description;
            newProduct.CategoryId = product.CategoryId;
            newProduct.ManufacturerId = product.ManufacturerId;

            _dbContext.Products.Update(newProduct);
            _dbContext.SaveChanges();

            return RedirectToAction("List","Product");
        }

        [RequireRole("admin")]
        public IActionResult Delete(int id)
        {
            Product? product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                TempData["Message"] = "Category with the specified ID was not found!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("List");
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            TempData["Message"] = "Product has been successfully deleted.";
            TempData["MessageType"] = "success";

            return RedirectToAction("List");
        }
    }
}

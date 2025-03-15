using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Web.Attributes;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;
using System.ComponentModel;

namespace MVC_Web.Controllers
{
    public class CategoryController : AuthController
    {
        private DatabaseContext _dbContext;

        public CategoryController(DatabaseContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        [RequireRole("admin")]
        public IActionResult Index()
        {
            List<CategoryViewModel> categories = _dbContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name, c.Description)).ToList();


            return View(categories);
        }

        [RequireRole("admin")]
        public IActionResult Add()
        {
            return View(new CategoryViewModel());
        }

        [RequireRole("admin")]
        [HttpPost]
        public IActionResult Add(CategoryViewModel category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            Category newCategory = new Category(
                    category.Id,
                    category.Name,
                    category.Description
                );

            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [RequireRole("admin")]
        public IActionResult Edit(int id)
        {
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                TempData["Message"] = "We coulnd't find this category!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            CategoryViewModel categoryViewModel = new CategoryViewModel(
                    category.Id,
                    category.Name,
                    category.Description
                );

            return View(categoryViewModel);
        }

        [RequireRole("admin")]
        [HttpPost]
        public IActionResult Edit(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category? newCategory = _dbContext.Categories.SingleOrDefault(c=>c.Id == category.Id);

            if (newCategory == null)
            {
                TempData["Message"] = "We coulnd't find this category!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            newCategory.Name = category.Name;
            newCategory.Description = category.Description;

            _dbContext.Categories.Update(newCategory);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [RequireRole("admin")]
        public IActionResult Delete(int id)
        {
            Category? category= _dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                TempData["Message"] = "Category with the specified ID was not found!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            TempData["Message"] = "Category has been successfully deleted.";
            TempData["MessageType"] = "success";

            return RedirectToAction("Index");
        }

    }
}

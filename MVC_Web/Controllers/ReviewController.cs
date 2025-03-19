using Microsoft.AspNetCore.Mvc;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;

namespace MVC_Web.Controllers
{
    public class ReviewController : BaseController
    {
        private DatabaseContext _dbContext;

        public ReviewController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int productId)
        {
            ReviewViewModel review = new ReviewViewModel();

            review.ProductId = productId;

            return View(review);
        }

        [HttpPost]
        public IActionResult Index(ReviewViewModel review)
        {
            if(!ModelState.IsValid)
            {
                return View(review);
            }

            Review newReview = new Review(
                review.Id,
                review.Date,
                review.Value,
                review.Description,
                review.ProductId,
                new Product()
                );

            _dbContext.Reviews.Add(newReview);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Product",new {id=review.ProductId});
        }
    }
}

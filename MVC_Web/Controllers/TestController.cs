using Microsoft.AspNetCore.Mvc;
using MVC_Web.Database;
using MVC_Web.Entities;

namespace MVC_Web.Controllers
{
    public class TestController : Controller
    {
        private DatabaseContext _dbContext;

        public TestController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            List<Review> reviews = _dbContext.Reviews.ToList();

            return View();
        }
    }
}

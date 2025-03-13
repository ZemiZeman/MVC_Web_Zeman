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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //List<Category> categories = _databaseContext.Categories.ToList();
            //List<Manufacturer> manufacturer = _databaseContext.Manufacturer.ToList();
            //List<Product> products = _databaseContext.Products.ToList();
            //List<Review> reviews = _databaseContext.Reviews.ToList();
            //List<OrdersDetail> ordersDetails = _databaseContext.OrdersDetails.ToList();
            //List<Order> orders = _databaseContext.Orders.ToList();
            //List<Delivery> deliveries = _databaseContext.Deliveries.ToList();
            //List<Payment> payments = _databaseContext.Payments.ToList();
            //List<Customer> customers = _databaseContext.Customers.ToList();

            return View();
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

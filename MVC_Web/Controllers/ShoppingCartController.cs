using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;
using MVC_Web.Models.Order;
using MySqlX.XDevAPI;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC_Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private DatabaseContext _dbContext;

        public ShoppingCartController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AddToCart(int productId)
        {
            Product p = _dbContext.Products.FirstOrDefault(p => p.Id == productId)!;

            if (p == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProductViewModel product = new ProductViewModel(p.Id, p.Name, p.Price, p.Description, p.CategoryId, p.ManufacturerId
                , new CategoryViewModel(p.Category.Id, p.Category.Name, p.Category.Description),
                new ManufacturerViewModel(p.Manufacturer.Id, p.Manufacturer.Name, p.Manufacturer.CounryOrigin));

            List<ProductViewModel> list = new List<ProductViewModel>();
            string? listJson = HttpContext.Session.GetString("Product");

            if (listJson != null)
            {
                list = JsonSerializer.Deserialize<List<ProductViewModel>>(listJson)!;
            }

            list.Add(product);

            listJson = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString("Product",listJson);

            return RedirectToAction("Index","Product",new {id=productId});
        }

        public IActionResult Index()
        {
            string? productJson = HttpContext.Session.GetString("Product");
            List<ProductViewModel> allProducts = new List<ProductViewModel>();

            if(productJson == null)
            {
                return View();
            }

            allProducts = JsonSerializer.Deserialize<List<ProductViewModel>>(productJson)!;

            List<OrdersDetailViewModel> ordersDetails = new List<OrdersDetailViewModel>();

            foreach (ProductViewModel product in allProducts)
            {
                OrdersDetailViewModel? ordersDetail = ordersDetails.FirstOrDefault(o=>o.ProductId == product.Id);

                if (ordersDetail != null)
                {
                    ordersDetail.ProductsCount++;
                }
                else
                {
                    OrdersDetailViewModel ordersDetailToAdd = new OrdersDetailViewModel();
                    ordersDetailToAdd.ProductId = product.Id;
                    ordersDetailToAdd.Product = product;
                    ordersDetailToAdd.ProductsCount = 1;
                    ordersDetails.Add(ordersDetailToAdd);
                }
            }

            string ordersDetailsJson = JsonSerializer.Serialize(ordersDetails);
            HttpContext.Session.SetString("OrdersDetail", ordersDetailsJson);

            return View(ordersDetails);
        }

        public IActionResult Order()
        {
            OrderViewModel orderViewModel = new OrderViewModel();

            List<PaymentViewModel> paymentList = _dbContext.Payments.Select(p => new PaymentViewModel(p.Id, p.Name)).ToList();
            List<DeliveryViewModel> deliveries = _dbContext.Deliveries.Select(d=> new DeliveryViewModel(d.Id, d.Name)).ToList();

            ViewBag.Payments = paymentList;
            ViewBag.Deliveries = deliveries;

            string? customerJson = HttpContext.Session.GetString("Customer");

            if (customerJson != null)
            {
                Customer customer = JsonSerializer.Deserialize<Customer>(customerJson)!;

                orderViewModel.CustomerFirstName = customer.FirstName;
                orderViewModel.CustomerLastName = customer.LastName;
                orderViewModel.CustomerPhone = customer.Phone;
                orderViewModel.CustomerEmail = customer.Email;
                orderViewModel.City = customer.City;
                orderViewModel.Address = customer.Address;
                orderViewModel.PSC = customer.PSC;

            }


            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult FinishOrder(OrderViewModel order)
        {

            string? ordersDetailJson = HttpContext.Session.GetString("OrdersDetail");

            if (ordersDetailJson == null)
            {
                ViewData["emptyCart"] = "true";
                return View();
            }

            List<OrdersDetailViewModel> ordersDetails = JsonSerializer.Deserialize<List<OrdersDetailViewModel>>(ordersDetailJson)!;

            Order newOrder = new Order();
            newOrder.CustomerFirstName = order.CustomerFirstName;
            newOrder.CustomerLastName = order.CustomerLastName;
            newOrder.CustomerPhone = order.CustomerPhone;
            newOrder.CustomerEmail = order.CustomerEmail;
            newOrder.City = order.City;
            newOrder.Address = order.Address;
            newOrder.PSC = order.PSC;
            newOrder.DeliveryId = order.DeliveryId;
            newOrder.PaymentId = order.PaymentId;
            newOrder.Payment = _dbContext.Payments.FirstOrDefault(p => p.Id == order.PaymentId)!;
            newOrder.Delivery = _dbContext.Deliveries.FirstOrDefault(d => d.Id == order.DeliveryId)!;
            newOrder.OrderState = "nová";

            if (ViewBag.IsAuthenticated)
            {
                Customer customer = JsonSerializer.Deserialize<Customer>(HttpContext.Session.GetString("Customer")!)!;
                newOrder.CustomerId = customer.Id;
            }


            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            int id = _dbContext.Orders.OrderByDescending(o=>o.ProcessingDate).Select(o => o.Id).First();

            foreach (OrdersDetailViewModel ordersDetail in ordersDetails)
            {
                ordersDetail.OrderId = id;
                OrdersDetail newOrdersDetail = new OrdersDetail();
                newOrdersDetail.ProductId = ordersDetail.ProductId;
                newOrdersDetail.OrderId = ordersDetail.OrderId;
                newOrdersDetail.ProductsCount = ordersDetail.ProductsCount;
                newOrdersDetail.TotalPrice = ordersDetail.TotalPrice;
                newOrdersDetail.Order = _dbContext.Orders.FirstOrDefault(o => o.Id == ordersDetail.OrderId)!;
                newOrdersDetail.Product = _dbContext.Products.FirstOrDefault(p=>p.Id == ordersDetail.ProductId)!;
                _dbContext.OrdersDetails.Add(newOrdersDetail);
            }
            _dbContext.SaveChanges();

            HttpContext.Session.Remove("OrdersDetails");
            HttpContext.Session.Remove("Product");
            

            return View();
        }
    }
}

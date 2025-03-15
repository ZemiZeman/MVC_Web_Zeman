using Microsoft.AspNetCore.Mvc;
using MVC_Web.Attributes;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Models;
using MVC_Web.Models.Auth;
using MVC_Web.Models.Order;
using System.Text.Json;

namespace MVC_Web.Controllers
{
    public class OrderController : AuthController
    {
        private DatabaseContext _dbContext;

        public OrderController(DatabaseContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult MyOrders()
        {
            Customer customer = JsonSerializer.Deserialize<Customer>(HttpContext.Session.GetString("Customer")!)!;

            List<Order> orders = _dbContext.Orders.Where(o => o.CustomerId == customer.Id).ToList();

            List<DeliveryViewModel> delivery = _dbContext.Deliveries.Select(d=> new DeliveryViewModel(d.Id,d.Name)).ToList();
            List<PaymentViewModel> payments = _dbContext.Payments.Select(p=> new PaymentViewModel(p.Id,p.Name)).ToList();

            List<MyOrderViewModel> myOrders = orders.Select(o=>new MyOrderViewModel(
                    o.Id,
                    o.ProcessingDate,
                    o.OrderState,
                    _dbContext.OrdersDetails.Where(od=>od.OrderId ==o.Id).Sum(od=>od.TotalPrice),
                    new PaymentViewModel(payments.SingleOrDefault(p=>p.Id==o.PaymentId)!.Id, payments.SingleOrDefault(p => p.Id == o.PaymentId)!.Name),
                    new DeliveryViewModel(delivery.SingleOrDefault(d=>d.Id==o.DeliveryId)!.Id, delivery.SingleOrDefault(d => d.Id == o.DeliveryId)!.Name))).ToList();

            TempData["ActionOfOrderDetail"] = "MyOrders";

            return View(myOrders);
        }

        public IActionResult OrderDetail(int id)
        {
            List<OrdersDetail> details = _dbContext.OrdersDetails.Where(od=>od.OrderId ==id).ToList();

            List<CategoryViewModel> categories = _dbContext.Categories.Select(c=>new CategoryViewModel(c.Id,c.Name,c.Description)).ToList();
            List<ManufacturerViewModel> manufacturers = _dbContext.Manufacturer.Select(m => new ManufacturerViewModel(m.Id, m.Name, m.CounryOrigin)).ToList();

            List<Product> productsObject = _dbContext.Products.ToList();

            List<ProductViewModel> products = productsObject.Select(p => new ProductViewModel(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Description,
                    p.CategoryId,
                    p.ManufacturerId,
                    categories.First(c=>c.Id == p.CategoryId),
                    manufacturers.First(m=>m.Id ==p.ManufacturerId)
                )).ToList();

            List<OrdersDetailViewModel> ordersDetailViewModels = details.Select(d => new OrdersDetailViewModel(
                    d.Id,
                    d.ProductId,
                    products.Single(p=>p.Id == d.ProductId),
                    d.OrderId,
                    d.ProductsCount
                )).ToList();

            return View(ordersDetailViewModels);
        }

        [RequireRole("admin")]
        public IActionResult AllOrders()
        {

            List<Order> orders = _dbContext.Orders.ToList();

            List<DeliveryViewModel> delivery = _dbContext.Deliveries.Select(d => new DeliveryViewModel(d.Id, d.Name)).ToList();
            List<PaymentViewModel> payments = _dbContext.Payments.Select(p => new PaymentViewModel(p.Id, p.Name)).ToList();

            List<MyOrderViewModel> allOrders = orders.Select(o => new MyOrderViewModel(
                    o.Id,
                    o.ProcessingDate,
                    o.OrderState,
                    _dbContext.OrdersDetails.Where(od => od.OrderId == o.Id).Sum(od => od.TotalPrice),
                    new PaymentViewModel(payments.SingleOrDefault(p => p.Id == o.PaymentId)!.Id, payments.SingleOrDefault(p => p.Id == o.PaymentId)!.Name),
                    new DeliveryViewModel(delivery.SingleOrDefault(d => d.Id == o.DeliveryId)!.Id, delivery.SingleOrDefault(d => d.Id == o.DeliveryId)!.Name))).ToList();

            TempData["ActionOfOrderDetail"] = "AllOrders";

            return View(allOrders);
        }

        [RequireRole("admin")]
        public IActionResult ChangeStateOfOrder(int id)
        {
            Order? order = _dbContext.Orders.SingleOrDefault(o => o.Id == id);

            if (order == null)
            {
                TempData["Message"] = "We coulnd't find this order!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("AllOrders");
            }


            ChangeOrderStateViewModel orderToChange = new ChangeOrderStateViewModel(
                    order.Id,
                    order.ProcessingDate,
                    order.OrderState,
                    _dbContext.OrdersDetails.Where(od => od.OrderId == order.Id).Sum(od => od.TotalPrice));
            
            return View(orderToChange);
        }

        [RequireRole("admin")]
        [HttpPost]
        public IActionResult ChangeStateOfOrder(ChangeOrderStateViewModel changeOrder)
        {
            Order? order = _dbContext.Orders.SingleOrDefault(o=>o.Id == changeOrder.Id)!;

            if (order == null)
            {
                TempData["Message"] = "We coulnd't find this order!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("AllOrders");
            }

            order.OrderState = changeOrder.OrderState;

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return RedirectToAction("AllOrders");

        }


    }
}

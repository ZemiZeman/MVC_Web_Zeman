using Microsoft.AspNetCore.Mvc;
using MVC_Web.Database;
using MVC_Web.Entities;
using MVC_Web.Helpers;
using MVC_Web.Models.Auth;
using System.Text.Json;

namespace MVC_Web.Controllers
{
    public class AuthController : BaseController
    {
        private DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            Customer? customer = _context.Customers.SingleOrDefault(c => c.Username == loginViewModel.Username);
            if (customer == null || customer.Password != SHA256Helper.HashPassword(loginViewModel.Password))
            {
                TempData["Message"] = "Wrong username or password!";
                TempData["MessageType"] = "danger";
                return View(loginViewModel);
            }

            string customerJson = JsonSerializer.Serialize(customer);
            HttpContext.Session.SetString("Customer", customerJson);

            HttpContext.Session.SetString("User", customer.Username);
            HttpContext.Session.SetString("Role", customer.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Špatně zadané údaje!";
                TempData["MessageType"] = "danger";
                return View(registerViewModel);
            }

            Customer? existingCustomer = _context.Customers.SingleOrDefault(c=>c.Username == registerViewModel.Username);
            if(existingCustomer != null)
            {
                TempData["Message"] = "Uživatelské jméno je již zabrané!";
                TempData["MessageType"] = "danger";
                return View(registerViewModel);
            }

            Customer customer = new Customer();

            customer.FirstName = registerViewModel.FirstName;
            customer.LastName = registerViewModel.LastName;
            customer.Username = registerViewModel.Username;
            customer.Password = SHA256Helper.HashPassword(registerViewModel.Password);
            customer.Phone = registerViewModel.Phone;
            customer.Email = registerViewModel.Email;
            customer.Address = registerViewModel.Address;
            customer.City = registerViewModel.City;
            customer.PSC = registerViewModel.PSC;
            customer.Role = registerViewModel.Role;


            _context.Customers.Add(customer);
            _context.SaveChanges();

            LoginViewModel login = new LoginViewModel
            {
                Username = registerViewModel.Username,
                Password = registerViewModel.Password
            };


            var loginResult = Login(login);


            return RedirectToAction("Index","Home");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Customer");

            return RedirectToAction("Index", "Home");
        }
    }
}

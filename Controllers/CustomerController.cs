using MarrubiumShop.Database;
using MarrubiumShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;

namespace MarrubiumShop.Controllers
{
    public class CustomerController : Controller
    {
        [Authorize]
        public IActionResult Main(string page)
        {
            if (Regex.IsMatch(page, @"[A-Z]"))
                return new NotFoundResult();
            else
                return View(page);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("reg_user_post")]
        public async Task<IActionResult> Registrate()
        {
            var customer = await Request.ReadFromJsonAsync<Customer>();
            using (var db = new marrubiumContext())
            {
                foreach (var c in db.Customers)
                {
                    if (c.CustomerEmail == customer.CustomerEmail)
                        return Json(new { Error = "email" });
                    else if (c.PhoneNumber == customer.PhoneNumber)
                        return Json(new { Error = "phone" });
                }
                if (customer.GetType().GetProperties().Any(p => p is null))
                    return Json(
                        new { Error = "При регистрации произошла ошибка, попробуйте снова" }, 
                        JsonDefaultOptions.Serializer);
                db.Customers.Add(customer);
                db.SaveChanges();
                return Json(
                    new { Message = "Вы были успешно зарегистрированы на сайте!" },
                    JsonDefaultOptions.Serializer);
            }
        }

        [HttpPost("login_user_post")]
        public async Task<IActionResult> Login()
        {
            var customer = await Request.ReadFromJsonAsync<Customer>();
            if (customer.CustomerEmail == "" || customer.CustomerPassword == "")
                return Json(
                    new { Error = "При входе на сайт произошла ошибка, попробуйте снова" },
                    JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                foreach (var c in db.Customers)
                {
                    if (c.CustomerEmail == customer.CustomerEmail || c.PhoneNumber == customer.PhoneNumber)
                    {
                        if (c.CustomerPassword != customer.CustomerPassword)
                            return Json(new { Error = "user-password" });
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, c.CustomerEmail) };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                        return Json(new { Result = "success" });
                    }
                }
                return Json(new { Error = "tel-or-email" });
            }
        }
    }
}

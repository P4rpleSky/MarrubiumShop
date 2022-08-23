using MarrubiumShop.Database;
using MarrubiumShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> RegistrateUser()
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
                    return Json(new { Error = "exception" });
                db.Customers.Add(customer);
                db.SaveChanges();
                return Json(new { Result = "success" });
            }
        }
    }
}

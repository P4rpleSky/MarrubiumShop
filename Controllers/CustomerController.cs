using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace MarrubiumShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Main(string page)
        {
            if (Regex.IsMatch(page, @"[A-Z]"))
                return new NotFoundResult();
            else
                return View(page);
        }
    }
}

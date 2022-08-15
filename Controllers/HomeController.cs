using MarrubiumShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MarrubiumShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main(string page)
        {
            if (page is null)
                return View("Main");
            else if (Regex.IsMatch(page, @"[A-Z]"))
                return new NotFoundResult();
            else if (page.ToLower() == "main")
                return new NotFoundResult();
            else
                return View(page);
        }
    }
}
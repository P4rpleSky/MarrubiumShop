using MarrubiumShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarrubiumShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
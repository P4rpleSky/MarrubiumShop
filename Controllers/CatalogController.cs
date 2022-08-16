using MarrubiumShop.Database;
using Microsoft.AspNetCore.Mvc;

namespace MarrubiumShop.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Main()
        {
            return View("Catalog");
        }

        [HttpGet("catalog.json")]
        public IActionResult GetProductsJSON()
        {
            using (var db = new marrubiumContext())
            {
                var products = db.Products.ToList();
                var jsonOptions = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return Json(products, jsonOptions);
            }
        }
    }
}

using MarrubiumShop.Database;
using MarrubiumShop.Database.Entitites;
using Microsoft.AspNetCore.Mvc;


namespace MarrubiumShop.Controllers
{
    public class CatalogController : Controller
    {
        private DatabaseService _databaseService;

        public CatalogController(IDatabaseService databaseService)
        {
            _databaseService = (DatabaseService)databaseService;
        }
        
        public IActionResult Main()
        {
            return View("Catalog");
        }

        [HttpGet("catalog.json")]
        public IActionResult GetAllProductsJson()
        {
            return Json(_databaseService.products, _databaseService.jsonOptions);
        }

        [Route("catalog/{id:int}")]
        public IActionResult Product(int id)
        {
            var currentProduct = _databaseService.products.FirstOrDefault(p => p.ProductId == id);
            if (currentProduct is null)
                return NotFound();
            return View();
        }

        [HttpGet("catalog/{id}/product.json")]
        public IActionResult GetProductJson(int id)
        {
            var currentProduct = _databaseService.products.FirstOrDefault(p => p.ProductId == id);
            if (currentProduct is null)
                return NotFound();
            List<Product> productAndRecommended = new List<Product>();
            var rand = new Random();
            var recommended = _databaseService.products
                .Where(p => (p.Function.Any(f => currentProduct.Function.Contains(f)) ||
                             p.SkinType.Any(f => currentProduct.SkinType.Contains(f)))
                             && p.ProductId != currentProduct.ProductId)
                .OrderBy(x => rand.Next())
                .Take(4);
            productAndRecommended.Add(currentProduct);
            productAndRecommended.AddRange(recommended);
            return Json(productAndRecommended, _databaseService.jsonOptions);
        }
    }
}

using MarrubiumShop.Database;
using MarrubiumShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MarrubiumShop.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Main()
        {
            return View("Catalog");
        }

        [HttpPost("catalog.json")]
        public async Task<IActionResult> GetAllProductsJson()
        {
            var sort = await Request.ReadFromJsonAsync<SortClass>();
            using (var db = new marrubiumContext())
            {
                IEnumerable<Product> products = db.Products;
                if (sort.Type != "Тип продукта" && sort.Type != "all")
                    products = products.Where(p => p.Type.Any(t => string.Equals(t, sort.Type, StringComparison.OrdinalIgnoreCase)));
                if (sort.Function != "По функции" && sort.Function != "all")
                    products = products.Where(p => p.Function.Any(f => string.Equals(f, sort.Function, StringComparison.OrdinalIgnoreCase)));
                if (sort.SkinType != "Тип кожи" && sort.SkinType != "all")
                    products = products.Where(p => p.SkinType.Any(s => string.Equals(s, sort.SkinType, StringComparison.OrdinalIgnoreCase)));
                if (sort.Order != "Сортировка" && sort.Order != "default")
                {
                    switch (sort.Order)
                    {
                        case "decs-price":
                            products = products.OrderByDescending(p => p.ProductPrice);
                            break;
                        case "acs-price":
                            products = products.OrderBy(p => p.ProductPrice);
                            break;
                        case "most-popular":
                            // To be impemented
                            break;
                        case "best-rate":
                            // To be impemented
                            break;
                    }
                }
                return Json(products.ToList(), JsonDefaultOptions.Serializer);
            }
        }

        [Route("catalog/{id:int}")]
        public IActionResult Product(int id)
        {
            using (var db = new marrubiumContext())
            {
                var currentProduct = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (currentProduct is null)
                    return NotFound();
                return View();
            }
        }

        [HttpGet("catalog/{id}/product.json")]
        public IActionResult GetProductJson(int id)
        {
            using (var db = new marrubiumContext())
            {
                var products = db.Products.ToList();
                var currentProduct = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (currentProduct is null)
                    return NotFound();
                List<Product> productAndRecommended = new List<Product>();
                var rand = new Random();
                var recommended = products
                    .Where(p => (p.Function.Any(f => currentProduct.Function.Contains(f)) ||
                                 p.SkinType.Any(f => currentProduct.SkinType.Contains(f)))
                                 && p.ProductId != currentProduct.ProductId)
                    .OrderBy(x => rand.Next())
                    .Take(4);
                productAndRecommended.Add(currentProduct);
                productAndRecommended.AddRange(recommended);
                return Json(productAndRecommended.ToList(), JsonDefaultOptions.Serializer);
            }
        }

        [HttpPut("{productId}/add_to_fav")]
        public IActionResult AddToFavourite(int productId)
        {
            var email = HttpContext.User.Identity.Name;
            if (email is null)
                return Json(
                        new { Error = "Вы не можете добавлять в избранное товары без авторизации на сайте!" },
                        JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == email);
                var favouriteProduct = db.CustomerFavourites
                    .FirstOrDefault(c => c.CustomerId == user.CustomerId && c.ProductId == productId);
                if (favouriteProduct != null)
                    return Json(
                        new { Error = "Этот продукт уже был добавлен в избранное!" },
                        JsonDefaultOptions.Serializer);
                var item = new CustomerFavourite()
                {
                    CustomerId = user.CustomerId,
                    ProductId = productId
                };
                db.CustomerFavourites.Add(item);
                db.SaveChanges();
            }
            return Json(new { Result = "success" });
        }

        [HttpDelete("{productId}/delete_from_fav")]
        public IActionResult DeleteFromFavourite(int productId)
        {
            var email = HttpContext.User.Identity.Name;
            if (email is null)
                return Json(
                        new { Error = "Вы не можете удалять товары из избранного без авторизации на сайте!" },
                        JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == email);
                var favouriteProduct = db.CustomerFavourites
                    .FirstOrDefault(c => c.CustomerId == user.CustomerId && c.ProductId == productId);
                if (favouriteProduct is null)
                    return Json(
                        new { Error = "Этот продукт уже был удален из избранного!" },
                        JsonDefaultOptions.Serializer);
                db.CustomerFavourites.Remove(favouriteProduct);
                db.SaveChanges();
            }
            return Json(new { Result = "success" });
        }
    }
}

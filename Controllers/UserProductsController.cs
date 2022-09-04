using MarrubiumShop.Database;
using MarrubiumShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarrubiumShop.Controllers
{
    [Route("userProducts")]
    public class UserProductsController : Controller
    {
        [HttpGet]
        public IActionResult GetProductsJson()
        {
            var identity = HttpContext.User.Identity;
            if (identity is null)
                return Json(
                        new List<Product>(),
                        JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == identity.Name);
                if (user is null)
                    return Json(
                            new { Error = "Пользователя не существует!" },
                            JsonDefaultOptions.Serializer);
                var favourites = db.CustomerFavourites
                    .Where(f => f.CustomerId == user.CustomerId)
                    .Select(f => f.Product);
                return Json(favourites.ToList(), JsonDefaultOptions.Serializer);
            }
        }

        [HttpPost]
        public IActionResult AddToUserProducts()
        {
            var productId = int.Parse(HttpContext.Request.Headers["ProductId"].ToString());
            var identity = HttpContext.User.Identity;
            if (identity is null)
                return Json(
                        new { Error = "Вы не можете добавлять в избранное товары без авторизации на сайте!" },
                        JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == identity.Name);
                if (user is null)
                    return Json(
                            new { Error = "Пользователя не существует!" },
                            JsonDefaultOptions.Serializer);
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

        [HttpDelete]
        public IActionResult DeleteFromUserProducts()
        {
            var productId = int.Parse(HttpContext.Request.Headers["ProductId"].ToString());
            var identity = HttpContext.User.Identity;
            if (identity is null)
                return Json(
                        new { Error = "Вы не можете удалять товары из избранного без авторизации на сайте!" },
                        JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == identity.Name);
                if (user is null)
                    return Json(
                            new { Error = "Пользователя не существует!" },
                            JsonDefaultOptions.Serializer);
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

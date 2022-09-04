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
            var productsCategory = HttpContext.Request.Headers["ProductsCategory"].ToString();
            if (productsCategory != "favourite" && productsCategory != "cart")
                return Json(new { Error = "Invalid category!" }, JsonDefaultOptions.Serializer);
            var identity = HttpContext.User.Identity;
            if (identity is null)
                return Json(new List<Product>(), JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == identity.Name);
                if (user is null)
                    return Json(new { Error = "Вы не авторизованы на сайте!" }, JsonDefaultOptions.Serializer);
                IQueryable<Product> products;
                if (productsCategory == "favourite")
                    products = db.CustomerFavourites
                        .Where(f => f.CustomerId == user.CustomerId)
                        .Select(f => f.Product);
                else
                    products = db.CustomerCarts
                        .Where(f => f.CustomerId == user.CustomerId)
                        .Select(f => f.Product);
                return Json(products.ToList(), JsonDefaultOptions.Serializer);
            }
        }

        [HttpPost]
        public IActionResult AddToUserProducts()
        {
            var productsCategory = HttpContext.Request.Headers["ProductsCategory"].ToString();
            if (productsCategory != "favourite" && productsCategory != "cart")
                return Json(new { Error = "Invalid category!" }, JsonDefaultOptions.Serializer);
            var productId = int.Parse(HttpContext.Request.Headers["ProductId"].ToString());
            var identity = HttpContext.User.Identity;
            if (identity is null)
                return Json(new { Error = "Вы не авторизованы на сайте!" }, JsonDefaultOptions.Serializer);
            using (var db = new marrubiumContext())
            {
                var user = db.Customers.FirstOrDefault(c => c.CustomerEmail == identity.Name);
                if (user is null)
                    return Json(new { Error = "Вы не авторизованы на сайте!" }, JsonDefaultOptions.Serializer);
                if (productsCategory == "favourite")
                {
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
                }
                else
                {
                    var cartProduct = db.CustomerCarts
                        .FirstOrDefault(c => c.CustomerId == user.CustomerId && c.ProductId == productId);
                    if (cartProduct != null)
                        return Json(
                            new { Error = "Этот продукт уже был добавлен в корзину!" },
                            JsonDefaultOptions.Serializer);
                    var item = new CustomerCart()
                    {
                        CustomerId = user.CustomerId,
                        ProductId = productId
                    };
                    db.CustomerCarts.Add(item);
                }  
                db.SaveChanges();
            }
            return Json(new { Result = "success" });
        }

        [HttpDelete]
        public IActionResult DeleteFromUserProducts()
        {
            var productsCategory = HttpContext.Request.Headers["ProductsCategory"].ToString();
            if (productsCategory != "favourite" && productsCategory != "cart")
                return Json(new { Error = "Invalid category!" }, JsonDefaultOptions.Serializer);
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
                            new { Error = "Вы не авторизованы на сайте!" },
                            JsonDefaultOptions.Serializer);
                if (productsCategory == "favourite")
                {
                    var favouriteProduct = db.CustomerFavourites
                        .FirstOrDefault(c => c.CustomerId == user.CustomerId && c.ProductId == productId);
                    if (favouriteProduct is null)
                        return Json(
                            new { Error = "Этот продукт уже был удален из избранного!" },
                            JsonDefaultOptions.Serializer);
                    db.CustomerFavourites.Remove(favouriteProduct);
                }
                else
                {
                    var cartProduct = db.CustomerCarts
                        .FirstOrDefault(c => c.CustomerId == user.CustomerId && c.ProductId == productId);
                    if (cartProduct is null)
                        return Json(
                            new { Error = "Этот продукт уже был удален из корзины!" },
                            JsonDefaultOptions.Serializer);
                    db.CustomerCarts.Remove(cartProduct);
                }
                db.SaveChanges();
            }
            return Json(new { Result = "success" });
        }
    }
}

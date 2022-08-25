using System;
using System.Collections.Generic;

namespace MarrubiumShop.Models
{
    public partial class Product
    {
        public Product()
        {
            CustomerCarts = new HashSet<CustomerCart>();
            CustomerFavourites = new HashSet<CustomerFavourite>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string[]? Type { get; set; }
        public string[]? Function { get; set; }
        public string[]? SkinType { get; set; }
        public string ImageName { get; set; } = null!;

        public virtual ICollection<CustomerCart> CustomerCarts { get; set; }
        public virtual ICollection<CustomerFavourite> CustomerFavourites { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

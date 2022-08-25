using System;
using System.Collections.Generic;

namespace MarrubiumShop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerCarts = new HashSet<CustomerCart>();
            CustomerFavourites = new HashSet<CustomerFavourite>();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CustomerPassword { get; set; }
        public string? CustomerEmail { get; set; }

        public virtual ICollection<CustomerCart> CustomerCarts { get; set; }
        public virtual ICollection<CustomerFavourite> CustomerFavourites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

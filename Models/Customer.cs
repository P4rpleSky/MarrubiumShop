using System;
using System.Collections.Generic;

namespace MarrubiumShop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerPassword { get; set; }
        public string? CustomerEmail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

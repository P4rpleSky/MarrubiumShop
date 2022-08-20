using System;
using System.Collections.Generic;

namespace MarrubiumShop.Models
{
    public partial class CustomerFavourite
    {
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Product? Product { get; set; }
    }
}

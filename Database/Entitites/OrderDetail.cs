﻿using System;
using System.Collections.Generic;

namespace MarrubiumShop.Database.Entitites
{
    public partial class OrderDetail
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public float? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public float? Discount { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}

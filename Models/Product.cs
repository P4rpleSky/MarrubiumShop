using System;
using System.Collections.Generic;

namespace MarrubiumShop.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string[]? Type { get; set; }
        public string[]? Function { get; set; }
        public string[]? SkinType { get; set; }
        public string? ImageName { get; set; }
    }
}

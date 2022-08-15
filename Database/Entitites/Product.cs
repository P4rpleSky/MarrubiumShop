namespace MarrubiumShop.Database.Entitites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductType Type { get; set; }
        public ProductFunction Function { get; set; }
        public SkinType SkinType { get; set; }
        public string ImagePath { get; set; }
    }
}

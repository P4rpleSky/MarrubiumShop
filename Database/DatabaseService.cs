using MarrubiumShop.Database.Entitites;
using System.Collections.Immutable;

namespace MarrubiumShop.Database
{
    public class DatabaseService : IDatabaseService
    {
        public readonly IReadOnlyList<Product> products;
        public readonly System.Text.Json.JsonSerializerOptions jsonOptions;

        public DatabaseService()
        {
            using (var db = new marrubiumContext())
            {
                products = ImmutableList.CreateRange(db.Products);
                jsonOptions = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
            }
        }
    }
}

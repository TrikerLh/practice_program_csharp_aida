using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart {
    internal class ShoppingCartSummary {
        public static string CreateSummary(List<Product> products, int totalProducts, decimal totalPrice)
        {
            var header = "Product name, Price with VAT, Quantity";
            var body = CreateBody(products);

            var footer = $"Total products: {totalProducts}\nTotal price: {totalPrice}\u20ac";
            return $"{header}\n{body}{footer}";
        }

        private static string CreateBody(List<Product> products)
        {
            if (!products.Any())
            {
                return "";
            }
            return $"{products[0].ProductName}, {products[0].ComputeCost()}€, 1\n";
        }
    }
}

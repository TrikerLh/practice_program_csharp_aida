using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart {
    internal class ShoppingCartSummary {
        private const string Header = "Product name, Price with VAT, Quantity\n";

        public static string CreateSummary(List<Product> products, decimal totalPrice)
        {
            return $"{Header}" +
                   $"{CreateBody(products)}" +
                   $"{CreateFooter(products, totalPrice)}";
        }

        private static string CreateFooter(List<Product> products, decimal totalPrice) {
            return $"Total products: {products.Count}\nTotal price: {totalPrice}\u20ac";
        }

        private static string CreateBody(List<Product> products)
        {
            if (!products.Any())
            {
                return "";
            }

            var productQuantity = products.Count;
            var productTotalCost = products[0].ComputeCost()*productQuantity;
            return $"{products[0].ProductName}, {productTotalCost}€, {productQuantity}\n";
        }
    }
}

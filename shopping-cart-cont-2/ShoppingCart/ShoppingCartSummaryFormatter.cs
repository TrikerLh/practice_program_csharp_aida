using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart {
    internal class ShoppingCartSummaryFormatter {
        private const string Header = "Product name, Price with VAT, Quantity\n";

        public static string CreateSummary(List<Product> products, decimal totalPrice) {
            var summary = new ShoppingCartSummary(products, totalPrice);
            return $"{Header}" +
                   $"{CreateBody(summary)}" +
                   $"{CreateFooter(summary)}";
        }

        private static string CreateFooter(ShoppingCartSummary summary) {
            return $"Total products: {summary.TotalProducts()}\nTotal price: {summary.TotalPrice()}\u20ac";
        }

        private static string CreateBody(ShoppingCartSummary summary)
        {
            if (summary.TotalProducts() == 0)
            {
                return "";
            }

            var productQuantity = summary.TotalProducts();
            var productTotalCost = summary.ProductsTotalCost();
            var productName = summary.ProductName();
            return $"{productName}, {productTotalCost}€, {productQuantity}\n";
        }
    }
}

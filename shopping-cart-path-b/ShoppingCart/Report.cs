using System.Collections.Generic;

namespace ShoppingCart;

public interface Report {
    IEnumerable<ReportLine> GetItems();
    decimal GetTotalPrice();
    int TotalProducts();
    bool ThereAreNoProducts();
    bool HasDiscount();
    decimal GetDiscount();
    string GetDiscountCode();
}
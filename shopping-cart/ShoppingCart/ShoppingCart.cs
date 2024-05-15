using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ShoppingCart
{
    private readonly ProductRepository _productRepository;
    private readonly DiscountRepository _discountRepository;
    private readonly CheckoutService _checkoutService;
    private readonly Display _display;
    private List<Product> products;

    public ShoppingCart(ProductRepository productRepository, DiscountRepository discountRepository, CheckoutService checkoutService, Display display)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _checkoutService = checkoutService;
        _display = display;
        products = new List<Product>();
    }

    public void AddItem(string productName)
    {
        var product = _productRepository.Get(productName);  

        if (product == null)
        {
            _display.Show("Producto no disponible.");
        }

        products.Add(product);
    }

    public void Checkout()
    {
        //var lines = new List<LineDto>();
        //foreach (var product in products) {
        //    if (!lines.Any(a => a.ProductName.Equals(product.Name))){
        //        lines.Add(CreateLine(product));
        //    }
        //    else
        //    {
        //        //var line = lines.Find(x => x.ProductName == product.Name);
        //        //lines.Add(new LineDto(product.Name, product.Cost, 1));

                
        //    }
        //}

        var lines = from product in products.Distinct()
            select new LineDto(product.Name, product.Cost, products.Count(p => p == product));

        _checkoutService.Checkout(new ShoppingCartDto(lines.ToList(), 0, lines.Sum(p => p.Cost*p.Qty)));
    }


    private static LineDto CreateLine(Product product)
    {
        return new LineDto(product.Name, product.Cost, 1);
    }
}
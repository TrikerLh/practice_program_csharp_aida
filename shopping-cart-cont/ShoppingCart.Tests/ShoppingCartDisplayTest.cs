using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ShoppingCart.Tests {
    public class ShoppingCartDisplayTest {

        //Lista Ejemplos
        //No hay productos en la cesta (mostrar cabecera y pies solamente)
        //Un producto sin promoción (no mostrar la línea promotion)
        //Un producto con promoción
        //Dos productos con una unidad (sumatorio)
        //Dos productos con varias unidad (sumatorio)

        private ProductsRepository _productsRepository;
        private ErrorNotifier _errorNotifier;
        private ShoppingCart _shoppingCart;
        private CheckoutService _checkoutService;
        private DiscountsRepository _discountsRepository;
        private Display _display;

        [SetUp]
        public void SetUp() {
            _productsRepository = Substitute.For<ProductsRepository>();
            _errorNotifier = Substitute.For<ErrorNotifier>();
            _checkoutService = Substitute.For<CheckoutService>();
            _discountsRepository = Substitute.For<DiscountsRepository>();
            _display = Substitute.For<Display>();
            _shoppingCart = new ShoppingCart(_productsRepository, _errorNotifier, _checkoutService, _discountsRepository, _display);
        }

        [Test]
        public void without_product() {

            _shoppingCart.Display();

            _display.Received(1).Show("Product name, Price with VAT, Quantity\nTotal products: 0\nTotal price: 0€");

        }
    }
}

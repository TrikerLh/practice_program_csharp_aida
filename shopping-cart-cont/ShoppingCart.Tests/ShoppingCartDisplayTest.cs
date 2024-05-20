using NSubstitute;
using NUnit.Framework;
using static ShoppingCart.Tests.ProductBuilder;

namespace ShoppingCart.Tests {
    public class ShoppingCartDisplayTest : ShoppingCartTest {
        private const string Iceberg = "Iceberg";

        //Lista Ejemplos
        //No hay productos en la cesta (mostrar cabecera y pies solamente)
        //Un producto sin promoción (no mostrar la línea promotion)
        //Un producto con promoción
        //Dos productos con una unidad (sumatorio)
        //Dos productos con varias unidad (sumatorio)

        [Test]
        public void without_product() {

            _shoppingCart.Display();

            _display.Received(1).Show("Product name, Price with VAT, Quantity\nTotal products: 0\nTotal price: 0€");
        }
    }
}

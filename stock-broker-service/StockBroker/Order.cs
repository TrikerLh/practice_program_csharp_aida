using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace StockBroker;

public class Order
{
    private readonly string _symbol;
    private readonly int _quantity;
    private readonly double _price;
    private readonly string _type;
    private bool _success;

    public Order()
    {
        _symbol = "";
        _quantity = 0;
        _price = 0.0;
        _type = "";
        _success = true;
    }
    public Order(string symbol, int quantity, double price, string type)
    {
        _symbol = symbol;
        _quantity = quantity;
        _price = price;
        _type = type;
        _success = true;
    }

    public double GetBuyAmount()
    {
        if (_type == "B")
        {
            return _quantity * _price;
        }

        return 0.0;
    }

    public double GetSellAmount() {
        if (_type == "S") {
            return _quantity * _price;
        }

        return 0.0;
    }

    public string GetSymbol()
    {
        return _symbol;
    }

    public int GetQuantity()
    {
        return _quantity;
    }

    public void ToFail()
    {
        _success = false;
    }

    public bool IsSuccess()
    {
        return _success;
    }

    public static List<Order> GetOrders(string orderSequence) {
        var orders = new List<Order>();
        if (string.IsNullOrEmpty(orderSequence)) {
            orders.Add(new Order());
            return orders;
        }
        var ordersSequence = orderSequence.Split(',');
        foreach (var order in ordersSequence) {
            var symbol = order.Split(" ")[0];
            var quantity = int.Parse(order.Split(" ")[1]);
            var price = double.Parse(order.Split(" ")[2], CultureInfo.InvariantCulture);
            var type = order.Split(" ")[3];
            orders.Add(new Order(symbol, quantity, price, type));
        }

        return orders;
    }
}
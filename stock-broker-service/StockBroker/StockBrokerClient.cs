using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private static CultureInfo _cultureInfo;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService)
    {
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
        _cultureInfo = new CultureInfo("en-US");
    }

    public void PlaceOrder(string orderSequence)
    {
        var time = _dateTimeProvider.GetDateTime();
        Order order = GetOrder(orderSequence);
        var summary = GetFormatSummary(time, order);
        _notifier.Notify(summary);
    }

    private Order GetOrder(string orderSequence)
    {
        if (string.IsNullOrEmpty(orderSequence))
        {
            return new Order("", 0, 0.0, "");
        }
        var symbol = orderSequence.Split(" ")[0];
        var quantity = int.Parse(orderSequence.Split(" ")[1]);
        var price = double.Parse(orderSequence.Split(" ")[2], _cultureInfo);
        var type = orderSequence.Split(" ")[3];
        var order = new Order(symbol, quantity, price, type);
        return order;
    }

    //TODO: es static, podría ir en una clase formater?
    private static string GetFormatSummary(DateTime time, Order order)
    {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (order.IsEmptyOrder())
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }
        var buyAmount = 0.0;
        var sellAmount = 0.0;
        if (order.IsBuy())
        {
            buyAmount = order.GetAmount();
        }
        else
        {
            sellAmount = order.GetAmount();
        }
        return timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
    }

    private static string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}

public class Order
{
    private readonly string _symbol;
    private readonly int _quantity;
    private readonly double _price;
    private readonly string _type;

    public Order(string symbol, int quantity, double price, string type)
    {
        _symbol = symbol;
        _quantity = quantity;
        _price = price;
        _type = type;
    }

    public bool IsBuy()
    {
        return _type == "B";
    }

    public double GetAmount()
    {
        return _quantity * _price;
    }

    public bool IsEmptyOrder()
    {
        return _symbol == "" && _quantity == 0 && _price == 0 && _type == "";
    }
}
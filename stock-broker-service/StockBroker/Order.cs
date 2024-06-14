namespace StockBroker;

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
    public bool IsEmptyOrder()
    {
        return _symbol == "" && _quantity == 0 && _price == 0 && _type == "";
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

    public int GetQuantity()
    {
        return _quantity;
    }

    public string GetSymbol()
    {
        return _symbol;
    }
}
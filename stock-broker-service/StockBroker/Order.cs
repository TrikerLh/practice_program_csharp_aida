namespace StockBroker;

public class Order
{
    private readonly string _symbol;
    private readonly int _quantity;
    private readonly double _price;
    private readonly string _type;
    private bool _success;

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
}
namespace StockBroker;

public interface StockBrokerService
{
    bool Place(OrderDTO order);
}
namespace ShoppingCart;

public interface Notifier
{
    void ShowError(string message);
    void Show(string message);
}
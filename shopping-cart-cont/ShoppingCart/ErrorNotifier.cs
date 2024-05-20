namespace ShoppingCart;

public interface ErrorNotifier
{
    void ShowError(string message);
    void Show(string message);
}
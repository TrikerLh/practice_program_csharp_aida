using System;

namespace ShoppingCart;

public record CheckoutDto
{
    private readonly decimal _totalCost;

    public CheckoutDto(decimal totalCost)
    {
        _totalCost = Math.Round(totalCost,2,MidpointRounding.ToPositiveInfinity);
    }

    public override string ToString() {
        return $"{nameof(_totalCost)}: {_totalCost}";
    }
}
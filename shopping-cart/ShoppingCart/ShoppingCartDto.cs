using System.Collections.Generic;

namespace ShoppingCart;

public record ShoppingCartDto(List<LineDto> Lines, double Discount, double TotalAmount);
using System.Globalization;

namespace CoffeeMachineApp.core;

public interface MessageConfiguration
{
    CultureInfo GetCultureInfo();
}
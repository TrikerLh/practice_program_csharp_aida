using System.Globalization;

namespace CoffeeMachineApp.core;

public interface MessageConfigurationProvider
{
    CultureInfo GetCultureInfo();
}
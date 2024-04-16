using System.Globalization;
using CoffeeMachineApp.core;
using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachineApp.Tests.core;

public class CultureInfoMessageNotificatorTest
{
    private CultureInfo messageCulture;
    private DrinkMakerDriver drinkMakerDriver;
    private CultureInfoMessageNotificator messageNotificator;

    [SetUp]
    public void SetUp()
    {
        messageCulture = new CultureInfo("en-GB");
        drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
        messageNotificator = new CultureInfoMessageNotificator(messageCulture, drinkMakerDriver);
    }

    [Test]
    public void Send_Select_Drink()
    {
        var expectedMessage = Message.Create("Please, select a drink!");

        messageNotificator.NotifySelectDrink();

        drinkMakerDriver.Received(1).Notify(expectedMessage);
    }
}
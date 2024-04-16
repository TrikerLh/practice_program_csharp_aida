﻿using System.Globalization;
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

        WhenNotifySelectDrink();

        ThenDrinkMakerDriverReceiveNotification(expectedMessage);
    }

    [Test]
    public void Send_Select_Drink_In_Spain()
    {
        var expectedMessage = Message.Create("Por favor, ¡selecciona una bebida!");

        messageNotificator = NotificatorSpain();
        WhenNotifySelectDrink();

        ThenDrinkMakerDriverReceiveNotification(expectedMessage);
    }

    [Test]
    public void Send_Select_Drink_In_Puerto_Rico()
    {
        var expectedMessage = Message.Create("Por favor, ¡selecciona una bebida!");

        messageNotificator = NotificatorPuertoRico();
        WhenNotifySelectDrink();

        ThenDrinkMakerDriverReceiveNotification(expectedMessage);
    }

    private CultureInfoMessageNotificator NotificatorSpain() => new(new CultureInfo("es-ES"), drinkMakerDriver);

    private CultureInfoMessageNotificator NotificatorPuertoRico() => new(new CultureInfo("es-PR"), drinkMakerDriver);

    private void WhenNotifySelectDrink()
    {
        messageNotificator.NotifySelectDrink();
    }

    private void ThenDrinkMakerDriverReceiveNotification(Message expectedMessage)
    {
        drinkMakerDriver.Received(1).Notify(expectedMessage);
    }
}
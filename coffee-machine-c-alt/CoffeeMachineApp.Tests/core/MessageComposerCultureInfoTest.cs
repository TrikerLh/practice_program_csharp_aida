using System.Globalization;
using CoffeeMachineApp.core;
using FluentAssertions;
using NUnit.Framework;

namespace CoffeeMachineApp.Tests.core;

public class MessageComposerCultureInfoTest
{
    private MessageComposerCultureInfo _messageComposer;

    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public void Get_Select_Drink_Message_In_Spanish()
    {
        _messageComposer = new MessageComposerCultureInfo(new CultureInfo("es-ES"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("¡Por favor, seleccione bebida!"));
    }

    [Test]
    public void Get_Select_Drink_Message_In_English()
    {
        _messageComposer = new MessageComposerCultureInfo(new CultureInfo("en-GB"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("Please, select drink!"));
    }

    [Test]
    public void Get_Missing_Money_In_England()
    {
        _messageComposer = new MessageComposerCultureInfo(new CultureInfo("en-GB"));
        var givenMissingAmount = 0.4m;

        var result = _messageComposer.ComposeMissingMoneyMessage(givenMissingAmount);

        result.Should().Be(Message.Create($"You are missing 0.4"));
    }

    [Test]
    public void Get_Missing_Money_In_Spain()
    {
        _messageComposer = new MessageComposerCultureInfo(new CultureInfo("es-ES"));
        var givenMissingAmount = 0.4m;

        var result = _messageComposer.ComposeMissingMoneyMessage(givenMissingAmount);

        result.Should().Be(Message.Create($"Moroso, paga lo que falta: 0,4 Primer aviso"));
    }

    [Test]
    public void Get_Missing_Money_In_Puerto_Rico()
    {
        _messageComposer = new MessageComposerCultureInfo(new CultureInfo("es-PR"));
        var givenMissingAmount = 0.4m;

        var result = _messageComposer.ComposeMissingMoneyMessage(givenMissingAmount);

        result.Should().Be(Message.Create($"Moroso, paga lo que falta: 0.4 Primer aviso"));
    }
}
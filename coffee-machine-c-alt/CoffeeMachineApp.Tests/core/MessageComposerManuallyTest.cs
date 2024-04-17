using System.Globalization;
using CoffeeMachineApp.core;
using FluentAssertions;
using NUnit.Framework;

namespace CoffeeMachineApp.Tests.core;

public class MessageComposerManuallyTest
{
    private MessageComposerManually _messageComposer;

    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public void Get_Select_Drink_Message_In_Spain()
    {
        _messageComposer = new MessageComposerManually(new CultureInfo("es-ES"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("¡Por favor, seleccione bebida!"));
    }

    [Test]
    public void Get_Select_Drink_Message_In_England()
    {
        _messageComposer = new MessageComposerManually(new CultureInfo("en-GB"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("Please, select drink!"));
    }
}
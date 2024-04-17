using System.Globalization;
using CoffeeMachineApp.core;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachineApp.Tests.core;

public class MessageComposerManuallyTest
{
    private MessageConfiguration _configuration;
    private MessageComposerManually _messageComposer;

    [SetUp]
    public void Setup()
    {
        _configuration = Substitute.For<MessageConfiguration>();
        _messageComposer = new MessageComposerManually(_configuration);
    }

    [Test]
    public void Get_Select_Drink_Message_In_Spain()
    {
        _configuration.GetCultureInfo().Returns(new CultureInfo("es-ES"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("¡Por favor, seleccione bebida!"));
    }

    [Test]
    public void Get_Select_Drink_Message_In_England()
    {
        _configuration.GetCultureInfo().Returns(new CultureInfo("en-GB"));

        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("Please, select drink!"));
    }
}
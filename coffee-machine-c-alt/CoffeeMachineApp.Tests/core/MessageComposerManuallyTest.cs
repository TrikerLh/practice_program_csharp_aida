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
        _messageComposer = new MessageComposerManually();
    }

    [Test]
    public void Get_Select_Drink_Message_In_Spain()
    {
        var result = _messageComposer.ComposeSelectDrinkMessage();

        result.Should().Be(Message.Create("¡Por favor, seleccione bebida!"));
    }
}
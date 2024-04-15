using System.Collections.Generic;
using System.Linq;
using CoffeeMachine.core;
using NSubstitute;
using NUnit.Framework;
using static CoffeeMachine.Tests.helpers.OrderBuilder;
using Message = CoffeeMachine.core.Message;

namespace CoffeeMachine.Tests.core;

public class CoffeeMachineTest
{
    private const string SelectDrinkMessage = "Please, select a drink!";
    private CoffeeMachine.core.CoffeeMachine _coffeeMachine;
    private DrinkMakerDriver _drinkMakerDriver;

    [SetUp]
    public void SetUp()
    {
        _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
        _coffeeMachine = new CoffeeMachine.core.CoffeeMachine(_drinkMakerDriver);
        _coffeeMachine.AddMoney(1000);
    }

    [Test]
    public void Make_Chocolate()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().Build());
    }

    [Test]
    public void Make_Tea()
    {
        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Make_Coffee()
    {
        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Coffee().Build());
    }

    [Test]
    public void Make_Any_Drink_With_1_Spoon_Of_Sugar()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(1).Build());
    }

    [Test]
    public void Make_Any_Drink_With_2_Spoons_Of_Sugar()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Make_Any_Drink_With_More_Than_2_Spoons_Of_Sugar()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Warns_The_User_When_No_Drink_Was_Selected()
    {
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create(SelectDrinkMessage));
    }

    [Test]
    public void Resets_Drink_After_Making_Drink()
    {
        AfterMakingDrink();

        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received(1).Send(Arg.Any<Order>());
        _drinkMakerDriver.Received().Notify(Message.Create(SelectDrinkMessage));
    }

    [Test]
    public void Resets_Sugar_After_Making_Drink()
    {
        var ordersSent = CaptureSentOrders();
        AfterMakingDrinkWithSugar();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        Assert.That(ordersSent.Last(), Is.EqualTo(Coffee().Build()));
    }

    private List<Order> CaptureSentOrders()
    {
        var ordersSent = new List<Order>();
        _drinkMakerDriver.Send(Arg.Do<Order>(order => ordersSent.Add(order)));
        return ordersSent;
    }

    private void AfterMakingDrink()
    {
        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();
    }

    private void AfterMakingDrinkWithSugar()
    {
        _coffeeMachine.SelectTea();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();
    }
}
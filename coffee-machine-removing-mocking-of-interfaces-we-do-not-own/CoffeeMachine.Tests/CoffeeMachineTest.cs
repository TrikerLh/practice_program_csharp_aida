using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests;

public class CoffeeMachineTest
{
    private CoffeeMachine _coffeeMachine;
    private DrinkMaker _drinkMaker;

    [SetUp]
    public void SetUp()
    {
        _drinkMaker = Substitute.For<DrinkMaker>();
        _coffeeMachine = new CoffeeMachine(_drinkMaker);
    }

    [Test]
    public void Serve_Hot_Chocolate()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("H::");
    }

    [Test]
    public void Serve_Tea()
    {
        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("T::");
    }

    [Test]
    public void Serve_Coffee()
    {
        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("C::");
    }

    [Test]
    public void Serve_Drink_With_One_Spoon_Of_Sugar()
    {
        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("H:1:0");
    }

    [Test]
    public void Serve_Drink_With_Two_Spoons_Of_Sugar()
    {
        _coffeeMachine.SelectTea();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("T:2:0");
    }

    [Test]
    public void Serve_Drink_With_More_Than_Two_Spoons_Of_Sugar()
    {
        _coffeeMachine.SelectCoffee();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMaker.Received().Execute("C:2:0");
    }
}
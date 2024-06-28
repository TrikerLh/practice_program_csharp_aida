using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace InspirationOfTheDay.Tests
{
    public class InspirationOfTheDayTest
    {
        [Test]
        public void Send_quote_to_employee()
        {
            var quotesService = Substitute.For<QuotesService>();
            var employeeRepository = Substitute.For<EmployeeRepository>();
            var sendService = Substitute.For<SendService>();
            var randomNumbersGenerator = Substitute.For<RandomNumbersGenerator>();
            quotesService.GetQuotes("mejor").Returns(new List<Quote> { new Quote("Frase con la palabra: mejor") });
            employeeRepository.GetEmployees().Returns(new List<Employee>  {new Employee("Pepe", new ContactData("email", "tlf"))});
            var inspirationOfTheDay = new InspirationOfTheDay(quotesService, employeeRepository,randomNumbersGenerator, sendService);

            inspirationOfTheDay.InspireSomeone("mejor");

            sendService.Received(1).Send(Arg.Is<Quote>(q => q.Text() == "Frase con la palabra: mejor"), 
                                    Arg.Is<Employee>(e => e.Name == "Pepe"));

        }

        [Test]
        public void Send_random_quote_to_employee() {
            var quotesService = Substitute.For<QuotesService>();
            var employeeRepository = Substitute.For<EmployeeRepository>();
            var sendService = Substitute.For<SendService>();
            var randomNumbersGenerator = Substitute.For<RandomNumbersGenerator>();
            quotesService.GetQuotes("avanzar").Returns(new List<Quote> { new Quote("Frase 1 con la palabra: avanzar"), new Quote("Frase 2 con la palabra: avanzar") });
            employeeRepository.GetEmployees().Returns(new List<Employee> { new Employee("Carmen", new ContactData("email", "tlf")) });
            randomNumbersGenerator.Get(Arg.Any<int>()).Returns(1);
            var inspirationOfTheDay = new InspirationOfTheDay(quotesService, employeeRepository, randomNumbersGenerator, sendService);

            inspirationOfTheDay.InspireSomeone("avanzar");

            sendService.Received(1).Send(Arg.Is<Quote>(q => q.Text() == "Frase 2 con la palabra: avanzar"),
                Arg.Is<Employee>(e => e.Name == "Carmen"));

        }
    }
}

/* LISTA DE EJEMPLOS
 * 1 - Escribir una palabra y que te devuelva una frase con dicha palabra de una lista.
 * 3 - Enviar la frase a un empleado aleatorio de una lista de emepleados.
 */

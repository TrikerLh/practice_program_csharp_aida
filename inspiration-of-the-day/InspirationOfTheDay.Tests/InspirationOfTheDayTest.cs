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
    }
}

/* LISTA DE EJEMPLOS
 * 1 - Escribir una palabra y que te devuelva una frase con dicha palabra de una lista.
 * 3 - Enviar la frase a un empleado aleatorio de una lista de emepleados.
 */

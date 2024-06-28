using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace InspirationOfTheDay.Tests
{
    public class InspirationOfTheDayTest
    {
        private QuotesService _quotesService;
        private EmployeeRepository _employeeRepository;
        private RandomNumbersGenerator _randomNumbersGenerator;
        private SendService _sendService;

        [SetUp]
        public void SetUp()
        {
            _quotesService = Substitute.For<QuotesService>();
            _employeeRepository = Substitute.For<EmployeeRepository>();
            _randomNumbersGenerator = Substitute.For<RandomNumbersGenerator>();
            _sendService = Substitute.For<SendService>();


        }
        [Test]
        public void Send_quote_to_employee()
        {
            _quotesService.GetQuotes("mejor").Returns(new List<Quote> { new Quote("Frase con la palabra: mejor") });
            _employeeRepository.GetEmployees().Returns(new List<Employee>  {new Employee("Pepe", new ContactData("email", "tlf"))});
            var inspirationOfTheDay = new InspirationOfTheDay(_quotesService, _employeeRepository,_randomNumbersGenerator, _sendService);

            inspirationOfTheDay.InspireSomeone("mejor");

            _sendService.Received(1).Send(Arg.Is<Quote>(q => q.Text == "Frase con la palabra: mejor"), 
                                    Arg.Is<Employee>(e => e.Name == "Pepe"));
        }

        [Test]
        public void Send_random_quote_to_employee() {
            _quotesService.GetQuotes("avanzar").Returns(new List<Quote> { new Quote("Frase 1 con la palabra: avanzar"), new Quote("Frase 2 con la palabra: avanzar") });
            _employeeRepository.GetEmployees().Returns(new List<Employee> { new Employee("Carmen", new ContactData("email", "tlf")) });
            _randomNumbersGenerator.Get(Arg.Any<int>()).Returns(1);
            var inspirationOfTheDay = new InspirationOfTheDay(_quotesService, _employeeRepository, _randomNumbersGenerator, _sendService);

            inspirationOfTheDay.InspireSomeone("avanzar");

            _sendService.Received(1).Send(Arg.Is<Quote>(q => q.Text == "Frase 2 con la palabra: avanzar"),
                Arg.Is<Employee>(e => e.Name == "Carmen"));
        }
    }
}

/* LISTA DE EJEMPLOS
 * 1 - Escribir una palabra y que te devuelva una frase con dicha palabra de una lista.
 * 3 - Enviar la frase a un empleado aleatorio de una lista de emepleados.
 */

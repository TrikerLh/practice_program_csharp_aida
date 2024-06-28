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
            _employeeRepository.GetEmployees().Returns(new List<Employee>  {new Employee("Pepe", new ContactData("email", "TlfPepe"))});
            var inspirationOfTheDay = new InspirationOfTheDay(_quotesService, _employeeRepository,_randomNumbersGenerator, _sendService);

            inspirationOfTheDay.InspireSomeone("mejor");

            _sendService.Received(1).Send(Arg.Is<QuoteDTO>(q => q.Text == "Frase con la palabra: mejor"), 
                                    Arg.Is<EmployeeConstactDTO>(e => e.Name == "Pepe" && e.Tlf == "TlfPepe"));
        }

        [Test]
        public void Send_random_quote_to_employee() {
            _quotesService.GetQuotes("avanzar").Returns(new List<Quote> { new Quote("Frase 1 con la palabra: avanzar"), new Quote("Frase 2 con la palabra: avanzar") });
            _employeeRepository.GetEmployees().Returns(new List<Employee> { new Employee("Carmen", new ContactData("email", "tlfCarmen")) });
            _randomNumbersGenerator.Get(Arg.Any<int>()).Returns(1, 0);
            var inspirationOfTheDay = new InspirationOfTheDay(_quotesService, _employeeRepository, _randomNumbersGenerator, _sendService);

            inspirationOfTheDay.InspireSomeone("avanzar");

            _sendService.Received(1).Send(Arg.Is<QuoteDTO>(q => q.Text == "Frase 2 con la palabra: avanzar"),
                Arg.Is<EmployeeConstactDTO>(e => e.Name == "Carmen" && e.Tlf == "tlfCarmen"));
        }

        [Test]
        public void Send_random_quote_to_random_employee() {
            _quotesService.GetQuotes("futuro").Returns(new List<Quote> { new Quote("Frase 1 con la palabra: futuro"), new Quote("Frase 2 con la palabra: futuro") });
            _employeeRepository.GetEmployees().Returns(new List<Employee> { new Employee("Carlos", new ContactData("email", "tlf")), new Employee("Luis", new ContactData("email", "tlfLuis")) });
            _randomNumbersGenerator.Get(Arg.Any<int>()).Returns(1, 1);
            var inspirationOfTheDay = new InspirationOfTheDay(_quotesService, _employeeRepository, _randomNumbersGenerator, _sendService);

            inspirationOfTheDay.InspireSomeone("futuro");

            _sendService.Received(1).Send(Arg.Is<QuoteDTO>(q => q.Text == "Frase 2 con la palabra: futuro"),
                Arg.Is<EmployeeConstactDTO>(e => e.Name == "Luis" && e.Tlf == "tlfLuis"));
        }
    }
}

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

    public class Quote
    {
        private readonly string _text;

        public Quote(string text)
        {
            _text = text;
        }

        public string Text()
        {
            return _text; 
        }
    }

    public class ContactData
    {
        private readonly string _email;
        private readonly string _tlf;

        public ContactData(string email, string tlf)
        {
            _email = email;
            _tlf = tlf;
        }
    }

    public class Employee
    {
        private readonly string _name;
        private readonly ContactData _contactData;

        public Employee(string name, ContactData contactData)
        {
            _name = name;
            _contactData = contactData;
        }

        public string Name => _name;
    }

    public interface RandomNumbersGenerator
    {
    }

    public interface SendService
    {
        void Send(Quote quote, Employee employee);
    }

    public interface EmployeeRepository
    {
        IList<Employee> GetEmployees();
    }


    public interface QuotesService
    {
        IList<Quote> GetQuotes(string word);
    }

    public class InspirationOfTheDay
    {
        private readonly QuotesService _quotesService;
        private readonly EmployeeRepository _employeeRepository;
        private readonly RandomNumbersGenerator _randomNumbersGenerator;
        private readonly SendService _sendService;

        public InspirationOfTheDay(QuotesService quotesService, EmployeeRepository employeeRepository, RandomNumbersGenerator randomNumbersGenerator, SendService sendService)
        {
            _quotesService = quotesService;
            _employeeRepository = employeeRepository;
            _randomNumbersGenerator = randomNumbersGenerator;
            _sendService = sendService;
        }

        public void InspireSomeone(string word)
        {
            var quotes = _quotesService.GetQuotes(word);
            var employees = _employeeRepository.GetEmployees();
            _sendService.Send(quotes[0], employees[0]);
        }
    }
}

/* LISTA DE EJEMPLOS
 * 1 - Escribir una palabra y que te devuelva una frase con dicha palabra de una lista.
 * 3 - Enviar la frase a un empleado aleatorio de una lista de emepleados.
 */

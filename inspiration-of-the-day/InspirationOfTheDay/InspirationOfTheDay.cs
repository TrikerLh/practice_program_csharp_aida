using System.Collections.Generic;

namespace InspirationOfTheDay;

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
        var numRandomQuote = _randomNumbersGenerator.Get(GetMaxNumber(quotes));
        var numRandomEmployee = _randomNumbersGenerator.Get(GetMaxNumber(employees));
        var quoteDto = new QuoteDTO(quotes[numRandomQuote].Text);
        var employeeWhatsAppConstactDto = GetEmployeeWhatsAppConstactDto(employees[numRandomEmployee]);
        _sendService.Send(quoteDto, employeeWhatsAppConstactDto);
    }

    private static EmployeeConstactDTO GetEmployeeWhatsAppConstactDto(Employee employee)
    {
        return new EmployeeConstactDTO(employee.Name, employee.ContactData.Tlf);
    }

    private static int GetMaxNumber<T>(IList<T> list)
    {
        return list.Count - 1;
    }
}
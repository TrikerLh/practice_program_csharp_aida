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
        _sendService.Send(quotes[0], employees[0]);
    }
}
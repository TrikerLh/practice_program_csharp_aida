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
        var numRandomQuote = _randomNumbersGenerator.Get(quotes.Count - 1);
        _sendService.Send(quotes[numRandomQuote], employees[0]);
    }
}
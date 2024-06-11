using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        [Test]
        public void Say_Buenos_dias_between_6AM_to_11_59_AM()
        {
            var outputService = Substitute.For<OutputService>();
            DateTimeProvider dateTimeProvider = Substitute.For<DateTimeProvider>();
            dateTimeProvider.Get().Returns(new TimeOnly(6, 00));
            HelloService helloService = new HelloService(outputService, dateTimeProvider);

            helloService.Hello();

            outputService.Received().Write("Buenos dias!");
        }
    }

    public class HelloService
    {
        private readonly OutputService _outputService;
        private readonly DateTimeProvider _dateTimeProvider;

        public HelloService(OutputService outputService, DateTimeProvider dateTimeProvider)
        {
            _outputService = outputService;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Hello()
        {
            _outputService.Write("Buenos dias!");
        }
    }

    public interface DateTimeProvider
    {
        public TimeOnly Get();
    }

    public interface OutputService
    {
        public string Write(string message);
    }
}


/* Lista de Test
 * Te saluda con "Buenos días!" desde las 6:00 AM hasta las 11:59AM
 * Te saluda con "Buenas tardes!" desde las 12:00 PM hasta las 07:59 PM
 * Te saluda con "Buenas noches!" desde las 08:00 PM hasta las 5:59 AM
 * */
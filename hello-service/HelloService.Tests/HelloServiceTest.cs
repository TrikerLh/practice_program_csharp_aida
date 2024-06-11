using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        private HelloService _helloService;
        private OutputService? _outputService;
        private DateTimeProvider? _dateTimeProvider;

        [SetUp]
        public void SetUp()
        {
            _outputService = Substitute.For<OutputService>();
            _dateTimeProvider = Substitute.For<DateTimeProvider>();
        }

        [Test]
        public void Say_Buenos_dias_between_6AM_to_11_59_AM()
        {
            _dateTimeProvider.Get().Returns(new TimeOnly(6, 00));
            _helloService = new HelloService(_outputService, _dateTimeProvider);

            _helloService.Hello();

            _outputService.Received().Write("Buenos dias!");
        }

        [Test]
        public void Say_Buenas_tardes_between_12AM_to_07_59_PM() {
            _dateTimeProvider.Get().Returns(new TimeOnly(12, 00));
            HelloService helloService = new HelloService(_outputService, _dateTimeProvider);

            helloService.Hello();

            _outputService.Received().Write("Buenas tardes!");
        }
    }
}


/* Lista de Test
 * Te saluda con "Buenos días!" desde las 6:00 AM hasta las 11:59AM OK
 * Te saluda con "Buenas tardes!" desde las 12:00 PM hasta las 07:59 PM
 * Te saluda con "Buenas noches!" desde las 08:00 PM hasta las 5:59 AM
 * */
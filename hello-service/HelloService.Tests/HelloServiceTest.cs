using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        private HelloService _helloService;
        private OutputService _outputService;
        private TimeProvider _timeProvider;

        [SetUp]
        public void SetUp()
        {
            _outputService = Substitute.For<OutputService>();
            _timeProvider = Substitute.For<TimeProvider>();
            _helloService = new HelloService(_outputService, _timeProvider);
        }

        [TestCase(6,0,"Buenos dias!")]
        [TestCase(11, 59, "Buenos dias!")]
        [TestCase(12, 00, "Buenas tardes!")]
        [TestCase(20, 00, "Buenas noches!")]
        public void Say_Hello(int hour, int minute, string message) {
            GetTime(hour, minute);

            _helloService.Hello();

            VerifyReceivedOnlyThis(message);
        }

        private void GetTime(int hour, int minute)
        {
            _timeProvider.Get().Returns(new TimeOnly(hour, minute));
        }

        private void VerifyReceivedOnlyThis(string message)
        {
            _outputService.Received(1).Write(message);
            _outputService.Received(1).Write(Arg.Any<string>());
        }
    }
}
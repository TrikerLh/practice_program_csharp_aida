using NSubstitute;
using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class ConsoleUserRequesterTest
    {
        [Test]
        public void request_user_data()
        {
            var inputRequester = Substitute.For<InputRequester>();
            var consoleUserDataRequester = new ConsoleUserDataRequester(inputRequester);
            var name = "Andrea";
            var fullName = "Perez";
            var password = "Andrea123";
            inputRequester.RequestInput("Enter a username").Returns(name);
            inputRequester.RequestInput("Enter your full name").Returns(fullName);
            inputRequester.RequestInput("Enter your password").Returns(password);
            inputRequester.RequestInput("Re-enter your password").Returns(password);

            var userData = consoleUserDataRequester.Request();

            var userDataExpected = new UserData(name,fullName,password,password);
            Assert.That(userData, Is.EqualTo(userDataExpected));
        }
    }
}
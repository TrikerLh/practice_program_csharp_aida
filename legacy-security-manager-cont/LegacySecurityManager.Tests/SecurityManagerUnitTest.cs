using NSubstitute;
using NUnit.Framework;
using System.Reflection.PortableExecutable;

namespace LegacySecurityManager.Tests
{
    public class SecurityManagerUnitTest
    {
        private const string Username = "Pepe";
        private const string FullName = "Pepe Garcia";
        private Notifier _notifier;
        private UserDataRequester _requester;
        private SecurityManager _securityManager;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<Notifier>();
            _requester = Substitute.For<UserDataRequester>();
            _securityManager = new SecurityManager(_notifier, _requester);
        }

        [Test]
        public void do_not_save_user_when_password_and_confirm_password_are_not_equals()
        {
            _requester.Request().Returns(new UserData(Username, FullName, "Pepe1234", "Pepe1234."));

            _securityManager.CreateValidUser();

            _notifier.Received(1).Notify("The passwords don't match");
        }

        [Test]
        public void do_not_save_user_when_password_too_short()
        {
            _requester.Request().Returns(new UserData(Username, FullName, "Pepe123", "Pepe123"));

            _securityManager.CreateValidUser();

            _notifier.Received(1).Notify("Password must be at least 8 characters in length");
        }

        [Test]
        public void save_user()
        {
            var validPassword = "Pepe1234";
            _requester.Request().Returns(new UserData(Username, FullName, validPassword, validPassword));

            _securityManager.CreateValidUser();

            var reversedPassword = "4321epeP";
            _notifier.Received(1).Notify($"Saving Details for User ({Username}, {FullName}, {reversedPassword})\n");
        }
    }
}

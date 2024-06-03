using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class SecurityManagerTest
    {
        [Test]
        public void Fail_When_Confirmed_Password_is_different_Than_Password()
        {
            var securityManager = new SecurityManagerForTesting(new List<string> { "UserName","Full Name", "Password1", "Password2" });

            securityManager.CreateSecurityUser();

            Assert.That(securityManager.messages, Has.Some.EquivalentTo("The passwords don't match"));
        }

        [Test]
        public void Fail_When_Passwords_Are_Less_Than_Eigth_Characters() {
            var securityManager = new SecurityManagerForTesting(new List<string> { "UserName", "Full Name", "Pass1", "Pass1" });

            securityManager.CreateSecurityUser();

            Assert.That(securityManager.messages, Has.Some.EquivalentTo("Password must be at least 8 characters in length"));
        }
        [Test]
        public void Create_User() {
            var securityManager = new SecurityManagerForTesting(new List<string> { "UserName", "Full Name", "Password1", "Password1" });

            securityManager.CreateSecurityUser();

            List<string> expectedMessages = new List<string>
            {
                "Enter a username",
                "Enter your full name",
                "Enter your password",
                "Re-enter your password",
                "Saving Details for User (UserName, Full Name, 1drowssaP)\n"
            };
            Assert.That(securityManager.messages, Is.EquivalentTo(expectedMessages));
        }

    }

    public class SecurityManagerForTesting : SecurityManager
    {
        private readonly List<string> _inputs;
        private int _count;
        public List<string> messages;
        public SecurityManagerForTesting(List<string> inputs)
        {
            _inputs = inputs;
            _count = 0;
            messages = new List<string>();
        }
        protected override string Read()
        {
            return _inputs[_count++];
        }

        protected override void Print(string message)
        {
            messages.Add(message);
        }
    }
}
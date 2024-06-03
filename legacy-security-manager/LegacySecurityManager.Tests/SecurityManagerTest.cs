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
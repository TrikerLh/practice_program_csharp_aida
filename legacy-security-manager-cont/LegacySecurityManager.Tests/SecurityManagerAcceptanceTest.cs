using NSubstitute;
using NUnit.Framework;
using System.Xml.Linq;

namespace LegacySecurityManager.Tests;

public class SecurityManagerAcceptanceTest
{
    private const string Username = "Pepe";
    private const string FullName = "Pepe Garcia";
    private Notifier _notifier;
    private SecurityManager _securityManager;
    private InputRequester _inputRequester;

    [SetUp]
    public void Setup()
    {
        _notifier = Substitute.For<Notifier>();
        _inputRequester = Substitute.For<InputRequester>();
        UserDataRequester requester = new ConsoleUserDataRequester(_inputRequester);
        _securityManager = new SecurityManager(_notifier, requester);
    }

    [Test]
    public void save_user()
    {
        var validPassword = "Pepe1234";
        _inputRequester.RequestInput("Enter a username").Returns(Username);
        _inputRequester.RequestInput("Enter your full name").Returns(FullName);
        _inputRequester.RequestInput("Enter your password").Returns(validPassword);
        _inputRequester.RequestInput("Re-enter your password").Returns(validPassword);

        _securityManager.CreateValidUser();

        var reversedPassword = "4321epeP";
        string lastMessage = $"Saving Details for User ({Username}, {FullName}, {reversedPassword})\n";
        _notifier.Received(1).Notify(lastMessage);
    }
}
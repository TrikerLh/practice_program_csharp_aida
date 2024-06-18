using NSubstitute;
using NSubstitute.Core;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    private IEmailService _emailService;
    private Product _polkaDotSocks;
    private CheckoutForTest _checkoutForTest;

    [SetUp]
    public void SetUp()
    {
        _emailService = Substitute.For<IEmailService>();
        _polkaDotSocks = new Product("Polka-dot Socks");
        _checkoutForTest = new CheckoutForTest(_polkaDotSocks, _emailService);
    }

    [Test]
    public void User_Not_Accept_Terms()
    {
        _checkoutForTest.TermsAndConditionConfirmation.WasAccepted().Returns(false);
        Assert.Throws<OrderCancelledException>(() => _checkoutForTest.ConfirmOrder());
    }

    [Test]
    public void User_Accept_Terms_And_Decline_News_Letter()
    {
        UserAcceptTermsAndConditions(true);
        UserAcceptNewsLetter(false);

        _checkoutForTest.ConfirmOrder();

        _emailService.DidNotReceive().SubscribeUserFor(Arg.Any<Product>());
    }

    [Test]
    public void User_Accept_Terms_And_Accept_News_Letter()
    {
        UserAcceptTermsAndConditions(true);
        UserAcceptNewsLetter(true);

        _checkoutForTest.ConfirmOrder();

        _emailService.Received(1).SubscribeUserFor(_polkaDotSocks);
    }


    private class CheckoutForTest : Checkout
    {
        private IList<UserConfirmation> UserConfirmations { get; } = new List<UserConfirmation>();
        public UserConfirmation NewsLetterConfirmation => UserConfirmations.First();
        public UserConfirmation TermsAndConditionConfirmation => UserConfirmations.Last();
        public CheckoutForTest(Product product, IEmailService emailService) : base(product, emailService)
        {
            
        }

        protected override UserConfirmation CreateUserConfirmation(string message)
        {

            UserConfirmation _userconfirmation = Substitute.For<UserConfirmation>();
            UserConfirmations.Add(_userconfirmation);
            return _userconfirmation;
        }
    }

    private ConfiguredCall UserAcceptNewsLetter(bool accept)
    {
        return _checkoutForTest.NewsLetterConfirmation.WasAccepted().Returns(accept);
    }

    private ConfiguredCall UserAcceptTermsAndConditions(bool accept)
    {
        return _checkoutForTest.TermsAndConditionConfirmation.WasAccepted().Returns(accept);
    }
}
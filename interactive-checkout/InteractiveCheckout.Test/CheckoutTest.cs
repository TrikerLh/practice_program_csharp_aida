using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    private IEmailService _emailService;
    private Product _polkaDotSocks;
    private CheckoutForTest _checkoutForTest;
    private UserConfirmation _newLetterSubscriberSubstitute;
    private UserConfirmation _termsAndConditionsAcceptedSubstitute;

    [SetUp]
    public void SetUp()
    {
        _emailService = Substitute.For<IEmailService>();
        _polkaDotSocks = new Product("Polka-dot Socks");
        _checkoutForTest = new CheckoutForTest(_polkaDotSocks, _emailService);
        _newLetterSubscriberSubstitute = _checkoutForTest.UserConfirmations.First().Value;
        _termsAndConditionsAcceptedSubstitute = _checkoutForTest.UserConfirmations.Last().Value;
    }

    [Test]
    public void User_Not_Accept_Terms()
    {
        _termsAndConditionsAcceptedSubstitute.WasAccepted().Returns(false);
        Assert.Throws<OrderCancelledException>(() => _checkoutForTest.ConfirmOrder());
    }

    [Test]
    public void User_Accept_Terms_And_Decline_News_Letter()
    {
        _termsAndConditionsAcceptedSubstitute.WasAccepted().Returns(true);
        _newLetterSubscriberSubstitute.WasAccepted().Returns(false);
        _emailService.DidNotReceive().SubscribeUserFor(Arg.Any<Product>());
    }


    private class CheckoutForTest : Checkout
    {
        public Dictionary<string, UserConfirmation> UserConfirmations { get; set; } = new Dictionary<string, UserConfirmation>();
        public CheckoutForTest(Product product, IEmailService emailService) : base(product, emailService)
        {
            
        }

        protected override UserConfirmation CreateUserConfirmation(string message)
        {

            UserConfirmation _userconfirmation = Substitute.For<UserConfirmation>();
            UserConfirmations.Add(message, _userconfirmation);
            return _userconfirmation;
        }
    }
}
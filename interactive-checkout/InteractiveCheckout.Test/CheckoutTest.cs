using NSubstitute;

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
        _checkoutForTest.TermsAndConditionConfirmation.WasAccepted().Returns(true);
        _checkoutForTest.NewsLetterConfirmation.WasAccepted().Returns(false);
        _emailService.DidNotReceive().SubscribeUserFor(Arg.Any<Product>());
    }


    private class CheckoutForTest : Checkout
    {
        private Dictionary<string, UserConfirmation> UserConfirmations { get; } = new Dictionary<string, UserConfirmation>();
        public UserConfirmation NewsLetterConfirmation => UserConfirmations.First().Value;
        public UserConfirmation TermsAndConditionConfirmation => UserConfirmations.Last().Value;
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
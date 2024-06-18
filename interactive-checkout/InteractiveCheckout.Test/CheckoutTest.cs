using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    [Test]
    public void User_Not_Accept_Terms()
    {
        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");

        var checkout = new CheckoutForTest(polkaDotSocks, emailService);
        var newLetterSubscriberSubstitute = checkout.UserConfirmations.First().Value;
        var termsAndConditionsAcceptedSubstitute = checkout.UserConfirmations.Last().Value;

        newLetterSubscriberSubstitute.WasAccepted().Returns(true);
        termsAndConditionsAcceptedSubstitute.WasAccepted().Returns(false);
        Assert.Throws<OrderCancelledException>(() => checkout.ConfirmOrder());
    }

    public class CheckoutForTest : Checkout
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
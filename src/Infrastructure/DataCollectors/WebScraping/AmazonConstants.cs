namespace Infrastructure.DataCollectors.WebScraping
{
    public static class AmazonConstants
    {
        public const string LoginPageUrl          = "https://www.amazon.de/gp/sign-in.html";
        public const string LoginPageTitle        = "Amazon Anmelden";
        public const string LoginFormSelector     = "form[name=signIn]";
        public const string LogoutHrefSelector    = "a[class=nav-hidden-aria][href^='/gp/flex/sign-out.html']";
        public const string HomePageTitle         = "Mein Konto";
        public const string OrderDetailsPageUrl   = "https://www.amazon.de/gp/your-account/order-details";
        public const string OrderDetailsPageTitle = "Bestelldetails";
    }
}
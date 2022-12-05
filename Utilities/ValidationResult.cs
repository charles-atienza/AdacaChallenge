namespace Adaca_Challenge.Utilities
{
    public record ValidationResult
    {
        public string Rule { get; set; }
        public string Message  { get; set; }
        public string Decision { get; set; } = "Unknown";
    }

    public static class RuleType
    {
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
        public const string EmailAddress = "emailAddress";
        public const string PhoneNumber = "phoneNumber";
        public const string BusinessNumer = "businessNumer";
        public const string LoanAmount = "loanAmount";
        public const string CitizenshipStatus = "CitizenshipStatus";
        public const string TimeTrading = "timeTrading";
        public const string CountryCode = "countryCode";
        public const string Industry = "industry";
    }
}

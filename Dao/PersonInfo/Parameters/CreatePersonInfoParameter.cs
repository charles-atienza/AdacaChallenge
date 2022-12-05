namespace Adaca_Challenge.Dao.PersonInfo.Parameters
{
    public record CreatePersonInfoParameter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public string CitizenshipStatus { get; set; }
        public string TimeTrading { get; set; }
        public string CountryCode { get; set; }
        public string Industry { get; set; }
    }
}

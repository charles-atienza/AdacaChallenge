using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Adaca_Challenge.Entities.PersonInfo
{
    public record PersonInfoModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        public string BusinessNumber { get; set; }
        [Required]
        [Precision(18, 3)]
        public decimal LoanAmount { get; set; }
        [Required]
        public string CitizenshipStatus { get; set; }
        [Required]
        public string TimeTrading { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string Industry { get; set; }
    }
}
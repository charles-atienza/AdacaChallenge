using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Utilities;
using Microsoft.Build.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adaca_Challenge.Dao.PersonInfo.Validator
{
    public static class CreatePersonInfoValidator
    {
        private const int MinimumLoanAmount = 10;
        private const int MaximumLoanAmount = 100;

        private const int MinTimeTrading = 1;
        private const int MaxTimeTrading = 20;

        private static readonly string[] validCitizenship = { "Citizen", "Permanent Resident" };
        private static readonly string[] allowedCountryCode = { "AU" };
        private static readonly string[] industries = { "Industry 1", "Industry 2", "Banned Industry 1", "Banned Industry 2"};

        public static async Task<ValidationResult[]> Validate(CreatePersonInfoParameter parameter)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            validationResults = ValidateName(validationResults, parameter);
            validationResults = ValidateContact(validationResults, parameter);

            validationResults = await ValidateBusinessNumber(RuleType.BusinessNumer, parameter.BusinessNumber, "Invalid Business number", validationResults);

            var isLoanAmountValid = parameter.LoanAmount > MinimumLoanAmount && parameter.LoanAmount < MaximumLoanAmount;
            validationResults = CreateValidationResult(RuleType.LoanAmount, isLoanAmountValid, "Invalid Loan Amount", validationResults);

            var isCitizenshipValid = validCitizenship.Contains(parameter.CitizenshipStatus);
            validationResults = CreateValidationResult(RuleType.CitizenshipStatus, isCitizenshipValid, "Invalid Citizenship status", validationResults);

            var isCountryCodeValid = allowedCountryCode.Contains(parameter.CountryCode);
            validationResults = CreateValidationResult(RuleType.CountryCode, isCountryCodeValid, "Invalid Country code", validationResults);


            /* Note: Time Trading CAN be a number. 
             * No statement was said what to do if it's not a number 
             * and it was why this is made to be valid by default.
             */
            var isTimeTradingValid = int.TryParse(parameter.TimeTrading, out var timeTrading);
            if(isTimeTradingValid && timeTrading > MinTimeTrading && timeTrading < MaxTimeTrading)
            { 
                validationResults = CreateValidationResult(RuleType.TimeTrading, false, "Invalid Time trading", validationResults);
            }

            validationResults = ValidateIndustry(RuleType.Industry, parameter.Industry, "Invalid Industry format", validationResults);

            return validationResults.ToArray();
        }

        private static List<ValidationResult> ValidateIndustry(string rule, string industry, string message, List<ValidationResult> validationResults)
        {
            var isIndustryValid = industries.Contains(industry);

            if (isIndustryValid)
            {
                if (industry.ToLower().Contains("banned"))
                {
                    var validationResult1 = new ValidationResult() { 
                        Rule = rule,
                        Message = message,
                        Decision = "Unqualified"
                    };

                    validationResults.Add(validationResult1);
                    return validationResults;
                }

                return validationResults;
            }

            var validationResult2 = new ValidationResult()
            {
                Rule = rule,
                Message = message
            };

            validationResults.Add(validationResult2);
            return validationResults;

        }
        private static List<ValidationResult> ValidateName(List<ValidationResult> validationResults, CreatePersonInfoParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.FirstName) && !string.IsNullOrEmpty(parameter.LastName))
            {
                validationResults = CreateValidationResult(RuleType.LastName, !String.IsNullOrEmpty(parameter.LastName), "Invalid Last name format", validationResults);
            }
            else if (string.IsNullOrEmpty(parameter.LastName) && !string.IsNullOrEmpty(parameter.FirstName))
            {
                validationResults = CreateValidationResult(RuleType.FirstName, !String.IsNullOrEmpty(parameter.FirstName), "Invalid First name format", validationResults);
            }
            else
            {
                validationResults = CreateValidationResult(RuleType.LastName, !String.IsNullOrEmpty(parameter.LastName), "Invalid Last name format", validationResults);
                validationResults = CreateValidationResult(RuleType.FirstName, !String.IsNullOrEmpty(parameter.FirstName), "Invalid First name format", validationResults);
            }

            return validationResults;
        }

        private static List<ValidationResult> ValidateContact(List<ValidationResult> validationResults, CreatePersonInfoParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.PhoneNumber) && !string.IsNullOrEmpty(parameter.EmailAddress))
            {
                validationResults = ValidateEmailAddress(RuleType.EmailAddress, parameter.EmailAddress, "Invalid Email address format", validationResults);
            }
            else if (string.IsNullOrEmpty(parameter.EmailAddress) && !string.IsNullOrEmpty(parameter.PhoneNumber))
            {
                validationResults = ValidatePhoneNumber(RuleType.PhoneNumber, parameter.PhoneNumber, "Invalid Phone number format", validationResults);
            }
            else
            {
                validationResults = ValidatePhoneNumber(RuleType.PhoneNumber, parameter.PhoneNumber, "Invalid Phone number format", validationResults);
                validationResults = ValidateEmailAddress(RuleType.EmailAddress, parameter.EmailAddress, "Invalid Email address format", validationResults);
            }

            return validationResults;
        }

        //Note: can be part of utilities
        private static List<ValidationResult> ValidatePhoneNumber(string rule, string phoneNumber, string message, List<ValidationResult> validationResults)
        {
            //checks for valid number
            var regex = new Regex(@"^(\+614)\d{8}$|^(02)\d{8}$|^(03)\d{8}$|^(04)\d{8}$|^(07)\d{8}|^(08)\d{8}$");

            if (!regex.Match(phoneNumber).Success)
            {
                var validationResult = new ValidationResult()
                {
                    Rule = rule,
                    Message = message,
                };
                validationResults.Add(validationResult);
            }

            return validationResults;
        }

        private static async Task<List<ValidationResult>> ValidateBusinessNumber(string rule, string businessNumber, string message, List<ValidationResult> validationResults)
        {
            var isBusinessNumberValid = businessNumber.Length == 11 && Int64.TryParse(businessNumber, out var businessNumberAsNumber);
            validationResults = CreateValidationResult(RuleType.BusinessNumer, isBusinessNumberValid, "Invalid Business number format", validationResults);

            return await Task.FromResult(validationResults);
        }

        private static List<ValidationResult> ValidateEmailAddress(string rule, string emailAddress, string message, List<ValidationResult> validationResults)
        {
            var regex = new Regex(@"^([a-z0-9]+(?:[._-][a-z0-9]+)*)@([a-z0-9]+(?:[.-][a-z0-9]+)*\.[a-z]{2,})$");
            if (!regex.IsMatch(emailAddress))
            {
                var validationResult = new ValidationResult()
                {
                    Rule = rule,
                    Message = message
                };
                validationResults.Add(validationResult);
            }

            return validationResults;
        }

        private static List<ValidationResult> CreateValidationResult(string rule, bool isValid, string message, List<ValidationResult> validationResults, string decision = null)
        {
            if (!isValid)
            {
                var validationResult = new ValidationResult()
                {
                    Rule = rule,
                    Message = message,
                    Decision = decision ?? "Unknown"
                };
                validationResults.Add(validationResult);
            }

            return validationResults;
        }
    }
}

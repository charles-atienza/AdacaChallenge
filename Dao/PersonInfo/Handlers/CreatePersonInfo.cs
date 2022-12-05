using Adaca_Challenge.Dao.PersonInfo.Interface;
using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Dao.PersonInfo.Validator;
using Adaca_Challenge.Entities.PersonInfo;
using Adaca_Challenge.Utilities;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Adaca_Challenge.Dao.PersonInfo.Handlers
{
    public class CreatePersonInfo : ICreatePersonInfoHandler
    {
        private readonly PersonInfoDbContext _personInfoContext;

        public CreatePersonInfo(PersonInfoDbContext personInfoDb)
        {
            _personInfoContext = personInfoDb;
        }

        public async Task<Result> Handle(CreatePersonInfoParameter parameter)
        {
            try
            {
                var validationResults = await CreatePersonInfoValidator.Validate(parameter);
                var result = new Result()
                {
                    Decision = ResultDecision.Qualified,
                    ValidationResults = validationResults
                };


                if (validationResults.Length == 0)
                {
                    var personInfo = _personInfoContext.PersonInfo
                    .Where(
                        x => x.FirstName == parameter.FirstName &&
                        x.LastName == parameter.LastName &&
                        x.EmailAddress == parameter.EmailAddress &&
                        x.PhoneNumber == parameter.PhoneNumber &&
                        x.BusinessNumber == parameter.BusinessNumber &&
                        x.LoanAmount == parameter.LoanAmount &&
                        x.CitizenshipStatus == parameter.CitizenshipStatus &&
                        x.TimeTrading == parameter.TimeTrading &&
                        x.CountryCode == parameter.CountryCode &&
                        x.Industry == parameter.Industry
                    ).FirstOrDefault();

                    if (personInfo is not null)
                    {
                        result.Decision = ResultDecision.Unqualified;
                        result.ValidationResults = new ValidationResult[]
                        {
                            new ValidationResult()
                            {
                                Rule = "database",
                                Message = "Already existing data",
                                Decision = "Unknown"
                            }
                        };
                        return result;
                    }


                    var newPersonInfo = new PersonInfoModel()
                    {
                        FirstName = parameter.FirstName,
                        LastName = parameter.LastName,
                        EmailAddress = parameter.EmailAddress,
                        PhoneNumber = parameter.PhoneNumber,
                        BusinessNumber = parameter.BusinessNumber,
                        LoanAmount = parameter.LoanAmount,
                        CitizenshipStatus = parameter.CitizenshipStatus,
                        TimeTrading = parameter.TimeTrading,
                        CountryCode = parameter.CountryCode,
                        Industry = parameter.Industry,
                    };
                    _personInfoContext.Add(newPersonInfo);
                    await _personInfoContext.SaveChangesAsync();

                    return result;
                }

                return new Result()
                {
                    Decision = ResultDecision.Unqualified,
                    ValidationResults = validationResults
                };
            }
            catch (Exception e)
            {
                var validationResults = new ValidationResult[] {
                    new ValidationResult()
                    {
                        Rule = ResultDecision.Unknown,
                        Message = e.Message,
                    }
                };

                return new Result()
                {
                    Decision = ResultDecision.Unknown,
                    ValidationResults = validationResults
                };
            }
        }
    }
}

using Adaca_Challenge.Dao.PersonInfo.Interface;
using Adaca_Challenge.Dao.PersonInfo.Handlers;
using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Entities.PersonInfo;
using Adaca_Challenge.Utilities;
using System.Threading.Tasks;

namespace Adaca_Challenge.Dao.PersonInfo
{
    public class PersonInfoService : IPersonInfoService
    {
        ICreatePersonInfoHandler createPersonInfo;

        public PersonInfoService(PersonInfoDbContext personInfoDb)
        {
            createPersonInfo = new CreatePersonInfo(personInfoDb);
        }

        public async Task<Result> CreatePersonInfo(CreatePersonInfoParameter parameters)
        {
            return await createPersonInfo.Handle(parameters);
        }
    }
}

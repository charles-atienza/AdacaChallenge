using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Adaca_Challenge.Dao.PersonInfo.Interface
{
    public interface IPersonInfoService
    {
        Task<Result> CreatePersonInfo(CreatePersonInfoParameter parameters);
    }
}

using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Entities.PersonInfo;
using Adaca_Challenge.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Adaca_Challenge.Dao.PersonInfo.Interface
{
    public interface ICreatePersonInfoHandler
    {
        Task<Result> Handle(CreatePersonInfoParameter parameter);
    }
}

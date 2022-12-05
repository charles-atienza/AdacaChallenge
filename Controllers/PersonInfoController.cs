using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adaca_Challenge.Entities.PersonInfo;
using Adaca_Challenge.Dao.PersonInfo.Interface;
using Adaca_Challenge.Dao.PersonInfo.Parameters;
using Adaca_Challenge.Utilities;

namespace Adaca_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInfoController : ControllerBase
    {
        private readonly IPersonInfoService _personInfoService;

        public PersonInfoController(IPersonInfoService service)
        {
            _personInfoService = service;
        }

        // GET: api/ToDoItems
        [HttpPost]
        public async Task<Result> CreatePersonInfo(CreatePersonInfoParameter parameters)
        {
            return await _personInfoService.CreatePersonInfo(parameters);
        }

    }
}

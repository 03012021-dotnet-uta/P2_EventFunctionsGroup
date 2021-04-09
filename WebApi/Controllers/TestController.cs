using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly TestLogic testLogic;
        public TestController(TestLogic sL)
        {
            testLogic = sL;
        }

        // [HttpPost("typeinit")]
        // public ActionResult<List<EventType>> InitTypes(List<String> types)
        // {
        //     List<EventType> newEvents = testLogic.InitTypes(types);
        //     return newEvents;
        // }
    }
}

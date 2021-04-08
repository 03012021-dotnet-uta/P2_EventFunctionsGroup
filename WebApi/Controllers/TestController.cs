using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
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


        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> allUsers = testLogic.GetUsers();
            return Ok(allUsers);
        }
    }
}

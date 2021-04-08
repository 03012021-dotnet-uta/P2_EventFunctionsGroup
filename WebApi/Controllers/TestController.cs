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


        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> allUsers = testLogic.GetUsers();
            return Ok(allUsers);
        }

        [HttpGet("login/{email}/{password}")]
        public ActionResult<List<User>> LogIn(string email, string password)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                User getUser = testLogic.GetUserByEmail(email, password);
                if(getUser == null)
                {
                    return StatusCode(450, "Invalid login");
                }
                else
                {
                    return Ok(getUser);
                }
            }
        }

        [HttpPost("register")]
        public ActionResult<User> RegisterUser(RawUser user)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                User newUser = testLogic.CreateUser(user);
                if(newUser == null) {
                    return StatusCode(401, "Failed to add to database");
                }
                return newUser;
            }
        }
        
        [HttpPost("register")]
        public ActionResult<User> RegisterEvent(RawUser user)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                User newUser = testLogic.CreateUser(user);
                if(newUser == null) {
                    return StatusCode(401, "Failed to add to database");
                }
                return newUser;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.RawModels;
using Logic;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserLogic userLogic;
        public UserController(UserLogic sL)
        {
            userLogic = sL;
        }

        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            List<User> allUsers = userLogic.GetUsers();
            return Ok(allUsers);
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
                User newUser = userLogic.CreateUser(user);
                if(newUser == null) {
                    return StatusCode(450, "Failed to add to database. User possibly already exists.");
                }
                return newUser;
            }
        }        

        [HttpGet("{id}")]
        public ActionResult<User> GetByID(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                User getUser = userLogic.GetUserByID(id);
                if(getUser == null)
                {
                    return StatusCode(400, "Couldn't find user");
                }
                else
                {
                    return getUser;
                }
            }
        }
    }
}

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

        /// <summary>
        /// Gets all existing users in database
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            List<User> allUsers = userLogic.GetUsers();
            return allUsers;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tries to logs in a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("login/{email}/{password}")]
        public ActionResult<User> LogIn(string email, string password)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                User getUser = userLogic.GetUserByEmail(email, password);
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

        /// <summary>
        /// Get all user information based off their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

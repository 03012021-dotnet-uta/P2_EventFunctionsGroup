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
    public class ManagerController : ControllerBase
    {
        private readonly ManagerLogic managerLogic;
        public ManagerController(ManagerLogic sL)
        {
            managerLogic = sL;
        }
        
        /// <summary>
        /// Gets all the created events by the manager
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns></returns>
        [HttpGet("getevents/{id}")]
        public ActionResult<string> GetAllCreatedEvents(Guid id)
        {
            return "Manager API Get";
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <returns></returns>
        [HttpPost("createevent")]
        public ActionResult<Event> CreateNewEvent(RawEvent userEvent)
        {
            Event newEvent = managerLogic.CreateNewEvent(userEvent);
            return newEvent;
        }

        /// <summary>
        /// Edits an old event
        /// </summary>
        /// <returns></returns>
        [HttpPut("editevent")]
        public ActionResult<string> EditEvent()
        {
            return "Manager API Get";
        }

        /// <summary>
        /// Gets total revenue data based off userID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet("getrevdata")]
        public ActionResult<string> GetAllRevenueInfo(Guid id)
        {
            return "Manager API Get";
        }

        /// <summary>
        /// Gets estimated revenue data based off event type
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns></returns>
        [HttpGet("getestdata")]
        public ActionResult<string> GetEstimatedData(Guid id)
        {
            return "Manager API Get";
        }

        /// <summary>
        /// Gets all the event types in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("eventtypes")]
        public ActionResult<string> GetEventTypes()
        {
            return "Manager API Get";
        }
    }
}

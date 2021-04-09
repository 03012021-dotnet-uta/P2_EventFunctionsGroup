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
        public ActionResult<List<RawPreviewEvent>> GetAllCreatedEvents(Guid id)
        {
            List<RawPreviewEvent> allEvents = managerLogic.GetAllEvents(id);
            return allEvents;
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <returns></returns>
        [HttpPost("createevent")]
        public async Task<ActionResult<Event>> CreateNewEvent(RawEvent userEvent)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                Event newEvent = await managerLogic.CreateNewEvent(userEvent);
                if(newEvent == null)
                {
                    return StatusCode(450, "Failed to make event. Invalid inputs.");
                }
                else
                {
                    return newEvent;
                }
                
            }
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
        public ActionResult<List<EventType>> GetEventTypes()
        {
            List<EventType> allTypes = managerLogic.GetEventTypes();
            return allTypes;
        }
    }
}

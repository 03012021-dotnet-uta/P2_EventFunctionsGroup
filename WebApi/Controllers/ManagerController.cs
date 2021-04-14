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
        public ActionResult<List<RawManagerEvent>> GetAllCreatedEvents(Guid id)
        {
            List<RawManagerEvent> allEvents = managerLogic.GetAllEvents(id);
            return allEvents;
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <returns></returns>
        [HttpPost("createevent")]
        public async Task<ActionResult<RawDetailEvent>> CreateNewEvent(RawEvent userEvent)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Failed to create models");
            }
            else
            {
                RawDetailEvent newEvent = await managerLogic.CreateNewEvent(userEvent);
                if(newEvent == null)
                {
                    return StatusCode(450, "Failed to make event. Invalid inputs(Possibly could not find address).");
                }
                else
                {
                    return newEvent;
                }
                
            }
        }

        /// <summary>
        /// Gets all users attending an event based off event id
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        [HttpGet("allattend/{id}")]
        public async Task<ActionResult<List<RawUser>>> GetAllAttending(Guid id)
        {
            List<RawUser> allUsers = await managerLogic.GetAllAttending(id);

            return allUsers;
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
        /// Deleted an event from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public ActionResult<bool> DeleteEvent(Guid id)
        {
            if(!managerLogic.DeleteEvent(id))
            {
                return StatusCode(450, "Failed to delete event.");
            }
            return true;
        }

        /// <summary>
        /// Gets total revenue data based off userID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet("getallrevdata/{id}")]
        public ActionResult<decimal> GetAllRevenueInfo(Guid id)
        {
            decimal totalRevenue = managerLogic.GetTotalRevenue(id);
            if(totalRevenue < 0)
            {
                return StatusCode(450, "Couldn't find user."); 
            }
            return totalRevenue;
        }

        /// <summary>
        /// Gets total revenue data based off userID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
        [HttpGet("getrevdata/${id}")]
        public ActionResult<decimal> GetRevenueForEvent(Guid id)
        {
            decimal totalRevenue = managerLogic.GetEventRevenue(id);
            if(totalRevenue < 0)
            {
                return StatusCode(450, "Couldn't find event."); 
            }
            return totalRevenue;
        }

        /// <summary>
        /// Gets estimated revenue data based off event type
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns></returns>
        [HttpGet("getestdata")]
        public async Task<ActionResult<RawEstData>> GetEstimatedData(Guid id)
        {
            RawEstData estData = await Task.Run(() => managerLogic.GetEstDataAsync(id));
            if(estData is null)
            {
                return StatusCode(450, "Could not find event type.");
            }
            return estData;
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

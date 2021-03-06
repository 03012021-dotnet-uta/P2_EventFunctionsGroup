using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Logic;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventLogic eventLogic;
        public EventController(EventLogic sL)
        {
            eventLogic = sL;
        }

        /// <summary>
        /// Gets all events(past and future)
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<RawPreviewEvent>>> GetAllAsync()
        {
            List<RawPreviewEvent> allUsers = await eventLogic.GetAllAsync();
            return allUsers;
        }

        /// <summary>
        /// Gets all upcoming events
        /// </summary>
        /// <returns></returns>
        [HttpGet("allupcoming")]
        public async Task<ActionResult<List<RawPreviewEvent>>> GetAllUpcomingAsync()
        {
            List<RawPreviewEvent> allUsers = await eventLogic.GetUpcomingEventsAsync();
            return allUsers;
        }

        /// <summary>
        /// Gets all events a user signed up for
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet("allsigned/{id}")]
        public async Task<ActionResult<List<RawPreviewEvent>>> GetAllSignedUpEventsAsync(Guid id)
        {
            List<RawPreviewEvent> allUsers = await eventLogic.GetAllSignedUpEventsAsync(id);
            return allUsers;
        }

        /// <summary>
        /// Gets all events a user has been to
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet("allprevious/{id}")]
        public async Task<ActionResult<List<RawPreviewEvent>>> GetAllPreviousEventsAsync(Guid id)
        {
            List<RawPreviewEvent> allUsers = await eventLogic.GetAllPreviousEventsAsync(id);
            return allUsers;
        }

        /// <summary>
        /// Gets all the reviews from an event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
        [HttpGet("allreviews/{id}")]
        public async Task<ActionResult<List<RawReviewToFE>>> GetAllReviews(Guid id)
        {
            List<RawReviewToFE> allUsers = await eventLogic.GetAllReviews(id);
            return allUsers;
        }

        [HttpPost("review")]
        public ActionResult<RawReviewToFE> CreateReview(RawReview review)
        {
            RawReviewToFE newReview = eventLogic.CreateReview(review);

            return newReview;
        }

        /// <summary>
        /// Gets details about an event based off id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("eventdetail/{id}")]
        public ActionResult<RawDetailEvent> GetEventDetails(Guid id)
        {
            RawDetailEvent getEvent = eventLogic.GetEventById(id);
            if(getEvent == null)
            {
                return StatusCode(450, "Invalid Inputs. Unable to create review.");
            }
            return getEvent;
        }
        
        /// <summary>
        /// Signs up a user for an event
        /// </summary>
        /// <param name="uid">User ID</param>
        /// <param name="eid">Event ID</param>
        /// <returns></returns>
        [HttpGet("signup/{uid}/{eid}")]
        public ActionResult<bool> SignupForEvent(Guid uid, Guid eid)
        {
            if(!eventLogic.EventSignUp(uid, eid))
            {
                return StatusCode(450, "Failed to signup. Event may be full. Please refresh and try again.");
            }

            return true;
        }

        /// <summary>
        /// Removes a user from the signup list for the event
        /// </summary>
        /// <param name="uid">User Id</param>
        /// <param name="eid">Event Id</param>
        /// <returns></returns>
        [HttpDelete("deletesignup/{uid}/{eid}")]
        public ActionResult<bool> UnregisterFromEvent(Guid uid, Guid eid)
        {
            if(!eventLogic.UnregisterFromEvent(uid, eid))
            {
                return StatusCode(450, "Failed to unregister. Invalid user/event.");
            }

            return true;
        }
    }
}

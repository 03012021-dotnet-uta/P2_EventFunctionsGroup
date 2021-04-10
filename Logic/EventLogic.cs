using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Repository;
using Repository.Repos;

namespace Logic
{
    public class EventLogic
    {
        private readonly EventRepo eventRepo;
        private readonly UserRepo userRepo;
        private readonly UsersEventRepo usersEventRepo;
        private readonly Mapper mapper = new Mapper();
        public EventLogic(EventRepo r, UserRepo u, UsersEventRepo ue)
        {
            eventRepo = r;
            userRepo = u;
            usersEventRepo = ue;
        }

        /// <summary>
        /// Gets all events that are coming up
        /// </summary>
        /// <returns></returns>
        public async Task<List<RawPreviewEvent>> GetUpcomingEventsAsync()
        {
            List<Event> upcomingEvents = await Task.Run(() => eventRepo.GetUpcomingEvents(DateTime.UtcNow));
            List<RawPreviewEvent> returnEvents = await ConvertAllEventsAsync(upcomingEvents);

            return returnEvents;
        }

        /// <summary>
        /// Gets all events in the database(past and future)
        /// </summary>
        /// <returns></returns>
        public async Task<List<RawPreviewEvent>> GetAllAsync()
        {
            List<Event> allEvents = await Task.Run(() => eventRepo.GetAllEvents());
            List<RawPreviewEvent> returnEvents = await ConvertAllEventsAsync(allEvents);

            return returnEvents;
        }

        /// <summary>
        /// Gets all reviews of an event based off eventID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
        public List<Review> GetAllReviews(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all events a user has been to from database based off userID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public async Task<List<RawPreviewEvent>> GetAllPreviousEventsAsync(Guid id)
        {
            List<Event> previousEvents = await Task.Run(() => eventRepo.GetPreviousEvents(DateTime.UtcNow));
            List<RawPreviewEvent> returnEvents = await ConvertAllEventsAsync(previousEvents);

            return returnEvents;
        }

        /// <summary>
        /// Gets all events a user signed up for from the database based off userID
        /// </summary>
        /// /// <param name="id">User ID</param>
        /// <returns></returns>
        public async Task<List<RawPreviewEvent>> GetAllSignedUpEventsAsync(Guid id)
        {
            ICollection<Event> allEvents = await Task.Run(() => eventRepo.GetSignedUpEvents(id));
            List<Event> filteredEvents = new List<Event>();
            foreach(Event e in allEvents)
            {
                await Task.Run(() => filteredEvents.Add(eventRepo.GetEventByID(e.Id)));
            }

            List<RawPreviewEvent> returnEvents = await ConvertAllEventsAsync(filteredEvents);

            return returnEvents;
        }

        /// <summary>
        /// Gets an event based off its id
        /// </summary>
        /// <param name="id">Store ID</param>
        /// <returns></returns>
        public RawDetailEvent GetEventById(Guid id)
        {
            Event getEvent = eventRepo.GetEventByID(id);
            int totalAttend = eventRepo.GetTotalAttend(id);
            RawDetailEvent detailEvent = mapper.EventToDetail(getEvent, totalAttend);
            
            return detailEvent;
        }

        /// <summary>
        /// Signs up a user for an event based off their IDs
        /// </summary>
        /// <param name="uid">User ID</param>
        /// <param name="eid">Event ID</param>
        /// <returns></returns>
        public bool EventSignUp(Guid uid, Guid eid)
        {
            Event tEvent = eventRepo.GetEventByID(eid);
            if(tEvent == null)
            {
                return false;
            }
            User user = userRepo.GetUserByID(uid);
            if(user == null)
            {
                return false;
            }

            UsersEvent signupUser = mapper.signUpById(uid, eid, user, tEvent);
            usersEventRepo.InsertUsersEvent(signupUser);

            return true;
        }

        /// <summary>
        /// Async converts a list of events to rawpreviewevents
        /// </summary>
        /// <param name="allEvents">List of Events</param>
        /// <returns></returns>
        private async Task<List<RawPreviewEvent>> ConvertAllEventsAsync(List<Event> allEvents)
        {
            List<RawPreviewEvent> returnEvents = new List<RawPreviewEvent>();
            List<Task<RawPreviewEvent>> tasks = new List<Task<RawPreviewEvent>>();
            foreach(Event e in allEvents)
            {
                tasks.Add(Task.Run(() => mapper.EventToPreview(e)));
            }
            var results = await Task.WhenAll(tasks);
            foreach(var item in results)
            {
                returnEvents.Add(item);
            }
            return returnEvents;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Repository;
using Repository.Repos;

namespace Logic
{
    public class ManagerLogic
    {
        private readonly UserRepo userRepo;
        private readonly EventRepo eventRepo;
        private readonly EventTypeRepo eventTypeRepo;
        private readonly LocationRepo locationRepo;
        private readonly UsersEventRepo usersEventRepo;
        private readonly Mapper mapper = new Mapper();
        public ManagerLogic(UserRepo r, EventRepo e, EventTypeRepo et, LocationRepo lr, UsersEventRepo ue)
        {
            userRepo = r;
            eventRepo = e;
            eventTypeRepo = et;
            locationRepo = lr;
            usersEventRepo = ue;
        }

        public async Task<Event> CreateNewEvent(RawEvent userEvent)
        {
            EventType type = await eventTypeRepo.GetEventTypeByIDAsync(userEvent.EventType);
            if(type is null)
            {
                return null;
            }
            Location existLoc = await Task.Run(() => locationRepo.GetLocationByAddress(userEvent.Street, userEvent.City + " " + userEvent.State + " " + userEvent.ZipCode));
            if(existLoc == null)
            {
                existLoc = await mapper.AddressToLocation(userEvent);
                if(existLoc == null)
                {
                    return null;
                }
            }
            User manager = await Task.Run(() => userRepo.GetUserByID(userEvent.ManagerID));
            Event newEvent = await mapper.RawToEvent(userEvent, type, existLoc, manager);
            newEvent = eventRepo.InsertEvent(newEvent);
            return newEvent;
        }

        public List<RawPreviewEvent> GetAllEvents(Guid id)
        {
            List<Event> allEvents = eventRepo.GetAllManagerEvents(id);
            List<RawPreviewEvent> allRawEvents = new List<RawPreviewEvent>();
            foreach(Event e in allEvents)
            {
                allRawEvents.Add(mapper.EventToPreview(e));
            }
            return allRawEvents;
        }

        public List<EventType> GetEventTypes()
        {
            List<EventType> allTypes = eventTypeRepo.GetAllEventTypes();

            return allTypes;
        }

        public async Task<List<RawUser>> GetAllAttending(Guid eid)
        {
            List<User> allAttending = await Task.Run(() => eventRepo.GetAllAttending(eid));
            List<RawUser> allUsers = await ConvertAllUsersToRawAsync(allAttending);

            return allUsers;
        }

        private async Task<List<RawUser>> ConvertAllUsersToRawAsync(List<User> allUsers)
        {
            List<RawUser> returnUsers = new List<RawUser>();
            List<Task<RawUser>> tasks = new List<Task<RawUser>>();
            foreach(User e in allUsers)
            {
                tasks.Add(Task.Run(() => mapper.UserToRaw(e)));
            }
            var results = await Task.WhenAll(tasks);
            foreach(var item in results)
            {
                returnUsers.Add(item);
            }
            return returnUsers;
        }
    }
}
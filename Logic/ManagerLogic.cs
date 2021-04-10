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
        private readonly Mapper mapper = new Mapper();
        public ManagerLogic(UserRepo r, EventRepo e, EventTypeRepo et)
        {
            userRepo = r;
            eventRepo = e;
            eventTypeRepo = et;
        }

        public async Task<Event> CreateNewEvent(RawEvent userEvent)
        {
            EventType type = await eventTypeRepo.GetEventTypeByIDAsync(userEvent.EventType);
            if(type is null)
            {
                return null;
            }
            Location loc = await mapper.AddressToLocation(userEvent);
            User manager = await Task.Run(() => userRepo.GetUserByID(userEvent.ManagerID));
            Event newEvent = await mapper.RawToEvent(userEvent, type, loc, manager);
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
    }
}
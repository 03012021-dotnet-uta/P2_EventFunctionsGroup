using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Repository;

namespace Logic
{
    public class ManagerLogic
    {
        private readonly TestRepository testRepo;
        private readonly Mapper mapper = new Mapper();
        public ManagerLogic(TestRepository r)
        {
            testRepo = r;
        }

        public async Task<Event> CreateNewEvent(RawEvent userEvent)
        {
            EventType type = await testRepo.GetEventTypeByID(userEvent.EventType);
            if(type is null)
            {
                return null;
            }
            Location loc = await mapper.AddressToLocation(userEvent);
            User manager = await Task.Run(() => testRepo.GetUserByID(userEvent.ManagerID));
            Event newEvent = await mapper.RawToEvent(userEvent, type, loc, manager);
            newEvent = testRepo.AddEvent(newEvent);
            return newEvent;
        }

        public List<RawPreviewEvent> GetAllEvents(Guid id)
        {
            List<Event> allEvents = testRepo.GetAllManagerEvents(id);
            List<RawPreviewEvent> allRawEvents = new List<RawPreviewEvent>();
            foreach(Event e in allEvents)
            {
                allRawEvents.Add(mapper.EventToPreview(e));
            }
            return allRawEvents;
        }

        public List<EventType> GetEventTypes()
        {
            List<EventType> allTypes = testRepo.GetAllEventTypes();

            return allTypes;
        }
    }
}
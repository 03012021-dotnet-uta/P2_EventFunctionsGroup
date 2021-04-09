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
            Console.WriteLine(userEvent.EventType);
            EventType type = await testRepo.GetEventTypeByID(userEvent.EventType);
            Console.WriteLine(type);
            if(type is null)
            {
                return null;
            }
            Location loc = await mapper.AddressToLocation(userEvent);
            User manager = await testRepo.GetUserByID(userEvent.ManagerID);
            Event newEvent = await mapper.RawToEvent(userEvent, type, loc, manager);
            newEvent = testRepo.AddEvent(newEvent);
            return newEvent;
        }

        public List<EventType> GetEventTypes()
        {
            List<EventType> allTypes = testRepo.GetAllEventTypes();

            return allTypes;
        }
    }
}
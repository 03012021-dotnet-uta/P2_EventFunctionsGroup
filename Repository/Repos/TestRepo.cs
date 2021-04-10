  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;

namespace Repository
{
    public class TestRepository
    {
        private readonly EventFunctionsContext context;

        public TestRepository(EventFunctionsContext c)
        {
            context = c;
        }

        public void AddLocation(Location loc)
        {
            context.Add<Location>(loc);
            context.SaveChanges();
        }

        public List<EventType> GetAllEventTypes()
        {
            List<EventType> allTypes = context.EventTypes.ToList();
            return allTypes;
        }

        public void InitEventTypes(EventType et)
        {
            context.Add<EventType>(et);
            context.SaveChanges();
        }



    }
}
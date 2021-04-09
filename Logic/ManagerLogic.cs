using System;
using System.Collections.Generic;
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
    }
}
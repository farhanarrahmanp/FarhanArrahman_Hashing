using System;
using API.Repositories.Interface;
using API.Context;
using API.Models;
using System.Linq;
using System.Collections.Generic;

namespace API.Repositories.Data
{
    public class DivisionRepository : IDivisionRepository
    {

        MyContext myContext;

        public DivisionRepository(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        public List<Division> Get()
        {
            var data = myContext.Divisions.ToList();
            return data;
        }

        public Division Get(int id)
        {
            var data = myContext.Divisions.Find(id);
            return data;
        }

        public int Post(Division division)
        {
            myContext.Divisions.Add(division);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Division division)
        {
            var data = Get(division.Id);
            data.Name = division.Name;
            myContext.Divisions.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var data = myContext.Divisions.Find(id);
            myContext.Divisions.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}

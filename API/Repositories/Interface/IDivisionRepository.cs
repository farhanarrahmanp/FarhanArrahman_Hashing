using System;
using API.Models;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IDivisionRepository
    {
        List<Division> Get();

        Division Get(int id);

        int Post(Division division);

        int Put(Division division);

        int Delete(int id);
    }
}

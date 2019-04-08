using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    interface IBeer
    {
        Beer GetById(int id);
        Beer GetByName(string name);
        Beer GetAllByStyleId(int id);
        IList<Beer> GetAll();
        IList<Beer> GetAll(int brewer_id);
    }
}

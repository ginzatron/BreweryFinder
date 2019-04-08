using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IBeer
    {
        Beer GetByBeerId(int id);
        Beer GetByName(string name);
        IList<Beer> GetAllByStyleId(int id);
        IList<Beer> GetAll();
        IList<Beer> GetAll(int brewery_id);
    }
}

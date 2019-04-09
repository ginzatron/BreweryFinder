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
        IList<Beer> GetBeers(string name, int style);
        IList<Beer> GetAll(int brewery_id);
    }
}

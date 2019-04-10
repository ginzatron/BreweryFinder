using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IBeerDAO
    {
        IList<Beer> GetBeers(string name, int style);
        IList<Beer> GetBeerByBrewery(int brewery_id);
    }
}

using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IBreweryDAO
    {
        Brewery GetById(int id);
        IList<Brewery> GetByName(string substring);
        IList<Brewery> GetAllByZip(int zip, string brewOrBar = "BarRestaurant");

        Brewery CreateBrewery(Brewery brewery);

    }

}

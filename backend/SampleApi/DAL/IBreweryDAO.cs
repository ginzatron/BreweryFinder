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
        IList<Brewery> GetAllByQuery(int? zip, string brewOrBar, string happyHour, string name, string userLat, string userLng, int? range, string beerName);
        Brewery CreateBrewery(Brewery brewery);
        Brewery GetBreweryByNameAddress(string name, string address);

    }
}

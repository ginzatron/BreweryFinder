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
        IList<Brewery> GetAllByQuery(int? zip = 0, string brewOrBar = "BarRestaurant", string happyHour = "00:00", string name = "");
        Brewery CreateBrewery(Brewery brewery);
    }
}

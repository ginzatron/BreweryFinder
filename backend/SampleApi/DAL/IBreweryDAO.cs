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
        Brewery GetByName(string name);
        IList<Brewery> GetAllByZip(int zip, string brewOrBar = "BarRestaurant");
    }
}

using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    interface IBrewery
    {
        Brewery GetById(int id);
        Brewery GetNyName(string name);
        IList<Brewery> GetAllByZip(int zip, string brewOrBar = "BarRestaurant");
    }
}

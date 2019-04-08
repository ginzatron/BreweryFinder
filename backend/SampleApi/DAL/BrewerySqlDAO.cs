using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;

namespace SampleApi.DAL
{
    public class BrewerySqlDAO : IBrewery
    {
        private string connectionString;
        public BrewerySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Brewery> GetAllByZip(int zip, string brewOrBar = "BarRestaurant")
        {
            throw new NotImplementedException();
        }

        public Brewery GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Brewery GetNyName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

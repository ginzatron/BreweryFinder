using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;

namespace SampleApi.DAL
{
    public class BeerSqlDAO : IBeer
    {
        private string connectionString;
        public BeerSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Beer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Beer> GetAll(int brewer_id)
        {
            throw new NotImplementedException();
        }

        public Beer GetAllByStyleId(int id)
        {
            throw new NotImplementedException();
        }

        public Beer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Beer GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

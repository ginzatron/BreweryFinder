using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;
using System.Data.SqlClient;

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
            IList<Beer> beers = new List<Beer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM beers ", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Beer beer = ConvertReaderToBeer(reader);
                        beers.Add(beer);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return beers;
        }

        private Beer ConvertReaderToBeer(SqlDataReader reader)
        {
            Beer beer = new Beer();

            beer.Id = Convert.ToInt32(reader["id"]);
            beer.Abv = Convert.ToDecimal(reader["abv"]);
            beer.Description = Convert.ToString(reader["description"]);
            beer.ImgSrc = Convert.ToString(reader["imgSrc"]);
            //beer.Style_id = Convert.ToInt32(reader["style_id"]);
            beer.Name = Convert.ToString(reader["name"]);
            
            return beer;
        }

        public IList<Beer> GetAll(int brewery_id)
        {
            IList<Beer> beers = new List<Beer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM beers
                                                    JOIN beers_breweries ON beers.id = beers_breweries.beer_id
                                                    JOIN breweries ON breweries.id = beers_breweries.brewery_id 
                                                    WHERE breweries.id = @brewery_id;", conn);
                    cmd.Parameters.AddWithValue("brewery_id", brewery_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Beer beer = ConvertReaderToBeer(reader);
                        beers.Add(beer);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return beers;
        }

        public IList<Beer> GetAllByStyleId(int id)
        {
            IList<Beer> beers = new List<Beer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM beers WHERE style_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Beer beer = ConvertReaderToBeer(reader);
                        beers.Add(beer);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return beers;
        }

        public Beer GetByBeerId(int id)
        {
            Beer beer = new Beer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM beers WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        beer = ConvertReaderToBeer(reader);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return beer;
        }

        public Beer GetByName(string name)
        {
            Beer beer = new Beer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM beers WHERE name = @name", conn);
                    cmd.Parameters.AddWithValue("@name", name);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        beer = ConvertReaderToBeer(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return beer;
        }
    }
}

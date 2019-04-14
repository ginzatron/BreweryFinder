﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;

namespace SampleApi.DAL
{
    public class BrewerySqlDAO : IBreweryDAO
    {
        private string connectionString;
        public BrewerySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Brewery> GetAllByQuery(int? zip = 0, string brewOrBar = "Both", string happyHour = "00:00", string name = "")
        {
            IList<Brewery> breweries = new List<Brewery>();
            bool isBar = brewOrBar != "brewery";
            bool isBrewery = brewOrBar != "bar";

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();  

                    cmd = new SqlCommand("select * from breweries where (@zip = 0 or zip = @zip) and isbar = @bar and isbrewery = @brewery and name like @name and (@happyHour = '00:00' or (@happyHour <= happyHourTo and @happyHour >= happyHourFrom))", conn);
                    cmd.Parameters.AddWithValue("@zip", zip);
                    cmd.Parameters.AddWithValue("@bar", isBar);
                    cmd.Parameters.AddWithValue("@brewery", isBrewery);
                    cmd.Parameters.AddWithValue("@name", '%' + name + '%');
                    cmd.Parameters.AddWithValue("@happyHour", happyHour);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Brewery brewery = ConvertReaderToBrewery(reader);
                        breweries.Add(brewery);
                    }
                
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return breweries;
        }

        private Brewery ConvertReaderToBrewery(SqlDataReader reader)
        {
            Brewery brewery = new Brewery();

            brewery.Id = Convert.ToInt32(reader["id"]);
            brewery.Name = Convert.ToString(reader["name"]);
            brewery.Established = Convert.ToInt32(reader["established"]);
            brewery.Address = Convert.ToString(reader["address"]);
            brewery.City = Convert.ToString(reader["city"]);
            brewery.State = Convert.ToString(reader["state"]);
            brewery.Zip = Convert.ToInt32(reader["zip"]);
            brewery.SiteURL = Convert.ToString(reader["siteurl"]);
            brewery.Description = Convert.ToString(reader["description"]);
            brewery.IsBar = Convert.ToBoolean(reader["isbar"]);
            brewery.IsBrewery = Convert.ToBoolean(reader["isbrewery"]);
            brewery.imgSrc = Convert.ToString(reader["imgSrc"]);
            brewery.Latitude = Convert.ToDecimal(reader["latitude"]);
            brewery.Longitude = Convert.ToDecimal(reader["longitude"]);
            
            if (!String.IsNullOrEmpty(Convert.ToString(reader["happyhourfrom"])))
            {
                brewery.HappyHourFrom = TimeSpan.Parse(Convert.ToString(reader["happyhourfrom"]));
                brewery.HappyHourTo = TimeSpan.Parse(Convert.ToString(reader["happyhourto"]));
            }

            return brewery;
        }

        public Brewery GetById(int id)
        {
            Brewery brewery = new Brewery();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT *, b.id beerId, b.description beerDesc, b.name beerName FROM BREWERIES br JOIN beers_breweries bb ON br.id=bb.brewery_id JOIN beers b ON b.id=bb.beer_id WHERE br.id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        brewery = ConvertReaderToBrewery(reader);
                    }
     

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return brewery;
        }

        public IList<Brewery> GetByName(string substring)
        {
            IList<Brewery> breweries = new List<Brewery>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from breweries where name like @name", conn);
                    cmd.Parameters.AddWithValue("@name", '%' + substring + '%');
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Brewery brewery = ConvertReaderToBrewery(reader);
                        breweries.Add(brewery);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return breweries;
        }
    }
}

using System;
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

        public IList<Brewery> GetAllByZip(int zip, string brewOrBar = "BarRestaurant")
        {
            IList<Brewery> breweries = new List<Brewery>();
            bool isBar = true;
            bool isBrewery = true;

            switch (brewOrBar)
            {
                case "bar":
                    isBar = true;
                    isBrewery = false;
                    break;
                case "brewery":
                    isBar = false;
                    isBrewery = true;
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();  

                    if (zip == 0)
                    {
                        cmd = new SqlCommand("select *, b.id beerId, b.description beerDesc, b.name beerName from breweries br JOIN beers_breweries bb ON br.id=bb.brewery_id JOIN beers b ON b.id=bb.beer_id ", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("select *, b.id beerId, b.description beerDesc, b.name beerName from breweries br JOIN beers_breweries bb ON br.id=bb.brewery_id JOIN beers b ON b.id=bb.beer_id where zip = @zip and isbar = @bar and isbrewery = @brewery", conn);

                    }
                    cmd.Parameters.AddWithValue("@zip", zip);
                    cmd.Parameters.AddWithValue("@bar", isBar);
                    cmd.Parameters.AddWithValue("@brewery", isBrewery);
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
            brewery.Latitude = Convert.ToDecimal(reader["latitude"]);
            brewery.Longitude = Convert.ToDecimal(reader["longitude"]);
            
            if (!String.IsNullOrEmpty(Convert.ToString(reader["happyhourfrom"])))
            {
                brewery.HappyHourFrom = TimeSpan.Parse(Convert.ToString(reader["happyhourfrom"]));
                brewery.HappyHourTo = TimeSpan.Parse(Convert.ToString(reader["happyhourto"]));
            }

            Beer beer = new Beer();

            beer.Id = Convert.ToInt32(reader["beerId"]);
            beer.Abv = Convert.ToDecimal(reader["abv"]);
            beer.Description = Convert.ToString(reader["beerDesc"]);
            //beer.ImgSrc = Convert.ToString(reader["imgSrc"]);
            beer.Style_id = Convert.ToInt32(reader["style_id"]);
            beer.Name = Convert.ToString(reader["beerName"]);

            brewery.beersAvailable = new List<Beer>();
            brewery.beersAvailable.Add(beer);

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
                        if (brewery.Id != Convert.ToInt32(reader["id"]))
                        {
                            brewery = ConvertReaderToBrewery(reader);
                        }
                        else
                        {
                            Beer beer = new Beer();

                            beer.Id = Convert.ToInt32(reader["beerId"]);
                            beer.Abv = Convert.ToDecimal(reader["abv"]);
                            beer.Description = Convert.ToString(reader["beerDesc"]);
                            //beer.ImgSrc = Convert.ToString(reader["imgSrc"]);
                            beer.Style_id = Convert.ToInt32(reader["style_id"]);
                            //beer.Style = Convert.ToString(reader["styles_name"]);
                            beer.Name = Convert.ToString(reader["beerName"]);

                            brewery.beersAvailable.Add(beer);
                        }
                    }
     

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return brewery;
        }

        public Brewery GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

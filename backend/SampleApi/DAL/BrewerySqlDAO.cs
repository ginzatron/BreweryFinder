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

        public IList<Brewery> GetAllByQuery(int? zip, string brewOrBar, string happyHour, string name, string userLat, string userLng, int? range, string beerName)
        {
            IList<Brewery> breweries = new List<Brewery>();
            bool isBar = brewOrBar != "brewery";
            bool isBrewery = brewOrBar != "bar";
            string sql = "select br.* from breweries br join beers_breweries bb ON br.id=bb.brewery_id JOIN beers b ON b.id = bb.beer_id where  (@zip = 0 or zip = @zip) and isbar = @bar and isbrewery = @brewery and br.name like @name and (@happyHour = '00:00' or (@happyHour <= happyHourTo and @happyHour >= happyHourFrom)) and (@beerName='' or b.name LIKE @beerName)" +
                         " group by br.id, br.name, br.happyHourFrom, br.happyHourTo, br.established, br.address, br.city, br.state, br.zip, br.latitude, br.longitude, br.siteURL, br.description, br.isBar, br.isBrewery, br.imgSrc";

            if (range != 0)
            {
                sql = "declare @source geography = geography::STPointFromText('Point(' + @lat + ' ' + @lng + ')', 4326)" +
                      " select br.* from breweries br join beers_breweries bb ON br.id=bb.brewery_id JOIN beers b ON b.id = bb.beer_id where (@zip = 0 or zip = @zip) and isbar = @bar and isbrewery = @brewery and br.name like @name and (@happyHour = '00:00' or (@happyHour <= happyHourTo and @happyHour >= happyHourFrom))" +
                      " and (@range = 0 or @source.STDistance(geography::STPointFromText('Point(' + cast(br.latitude as nvarchar(25)) + ' ' + cast(br.longitude as nvarchar(25)) + ')', 4326)) * 0.000621371 <= @range) and (@beerName='' or b.name LIKE @beerName)" +
                      " group by br.id, br.name, br.happyHourFrom, br.happyHourTo, br.established, br.address, br.city, br.state, br.zip, br.latitude, br.longitude, br.siteURL, br.description, br.isBar, br.isBrewery, br.imgSrc";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);  

                    cmd.Parameters.AddWithValue("@zip", zip);
                    cmd.Parameters.AddWithValue("@bar", isBar);
                    cmd.Parameters.AddWithValue("@brewery", isBrewery);
                    cmd.Parameters.AddWithValue("@name", '%' + name + '%');
                    cmd.Parameters.AddWithValue("@happyHour", happyHour);
                    cmd.Parameters.AddWithValue("@lat", userLat);
                    cmd.Parameters.AddWithValue("@lng", userLng);
                    cmd.Parameters.AddWithValue("@range", range);
                    cmd.Parameters.AddWithValue("@beerName", '%' + beerName + '%');
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

        /// <summary>
        /// Creates a New Brewery
        /// </summary>
        /// <param name="brewery">The brewery object</param>
        /// <returns>If successful, the newly created brewery with its primary key id</returns>
        public Brewery CreateBrewery(Brewery brewery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO breweries(name, address, city, latitude, longitude, siteUrl) VALUES (@name, @address, @city,  @latitude, @longitude, @siteUrl); SELECT @@IDENTITY;", conn);
                    cmd.Parameters.AddWithValue("@name", brewery.Name);
                    cmd.Parameters.AddWithValue("@address", brewery.Address);
                    cmd.Parameters.AddWithValue("@city", brewery.State);
                    cmd.Parameters.AddWithValue("@zip", brewery.Zip);
                    cmd.Parameters.AddWithValue("@latitude", brewery.Longitude);
                    cmd.Parameters.AddWithValue("@longitude", brewery.Latitude);
                    cmd.Parameters.AddWithValue("@siteUrl", brewery.SiteURL);

                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    brewery.Id = id;
                    return brewery;

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error occured while creating new brewery");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IList<Brewery> GetWithinRadiusMiles(string userLat, string userLng, int miles)
        {
            IList<Brewery> breweries = new List<Brewery>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("declare @source geography = geography::STPointFromText('Point(' + @lat + ' ' + @lng + ')', 4326)" +
                                                    "select * from breweries br" +
                                                    "where @source.STDistance(geography::STPointFromText('Point(' + cast(br.latitude as nvarchar(25)) + ' ' + cast(br.longitude as nvarchar(25)) + ')', 4326)) * 0.000621371 <= @miles", conn);

                    cmd.Parameters.AddWithValue("@lat", userLat);
                    cmd.Parameters.AddWithValue("@lng", userLng);
                    cmd.Parameters.AddWithValue("@miles", miles);
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

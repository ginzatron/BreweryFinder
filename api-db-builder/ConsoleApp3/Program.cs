using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleApi.DAL;
using SampleApi.Models;
using System.Data.SqlClient;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            IBreweryDAO breweryDao = new BrewerySqlDAO("Data Source=.\\SQLEXPRESS;Initial Catalog=BreweryDB;Integrated Security=True");
            WebClient client = new WebClient();
            string urltemplate = "https://api.openbrewerydb.org/breweries?by_state=ohio&per_page=50&page={0}";
            int page = 0;
            bool morePages = true;
            while (morePages)
            {
                page++;
                string url = string.Format(urltemplate, page);
                string response = client.DownloadString(url);
                JArray array = (JArray)JsonConvert.DeserializeObject(response);
                if (array.Count < 50)
                {
                    morePages = false;
                }
                foreach (JObject obj in array)
                {
                    if (!obj.ContainsKey("latitude") || !obj.ContainsKey("longitude") ||
                        obj["latitude"].Type == JTokenType.Null || obj["longitude"].Type == JTokenType.Null)
                    {
                        //continue will skip a particular iteration and then 
                        //resume on the following iteration
                        continue;
                    }

                    Console.WriteLine($"{obj["name"]}: {obj["street"]}");
                    //Console.ReadLine();


                    Brewery brewery = new Brewery();
                    string breweryname = (string)obj["name"];
                    string address = (string)obj["street"];
                    if (breweryDao.GetBreweryByNameAddress(breweryname, address) != null)
                    {
                        continue;
                    }
                    brewery.Name = (string)obj["name"];
                    brewery.Address = (string)obj["street"];
                    brewery.City = (string)obj["city"];
                    //brewery.State = (string)obj["state;"];
                    brewery.State = "OH";
                    //re: the value of zip, DB has to be varchar not int

                    string zip5 = (string)obj["postal_code"];
                    zip5 = zip5.Length > 5 ? zip5.Substring(0, 5) : zip5;
                    int zip;
                    if (int.TryParse(zip5, out zip))
                    {
                        brewery.Zip = zip;
                    }
                    else
                    {
                        brewery.Zip = 0;
                    }
                    brewery.Latitude = (decimal)obj["latitude"];
                    brewery.Longitude = (decimal)obj["longitude"];
                    brewery.SiteURL = (string)obj["website_url"];
                    brewery.imgSrc = "https://gdurl.com/3bJ5";
                    brewery.Established = 1900;
                    brewery.Description = "";
                    brewery.IsBar = false;
                    brewery.IsBrewery = true;

                    Brewery newBrewery = breweryDao.CreateBrewery(brewery);

                }
            }

        }
    }
}

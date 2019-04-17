using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private IBreweryDAO breweryDao;
        private IBeerDAO beerDao;

        public BreweryController(IBreweryDAO breweryDao, IBeerDAO beerDao)
        {
            this.breweryDao = breweryDao;
            this.beerDao = beerDao;
        }

        [HttpGet]
        public ActionResult<List<Brewery>> GetBreweries([FromQuery]int? zip, [FromQuery]string brewOrBar, [FromQuery]string happyHour, [FromQuery]string name, [FromQuery]string userLat, [FromQuery]string userLng, [FromQuery]int? range, [FromQuery]string beerName)
        {
            if (zip == null)
            {
                zip = 0;
            }
            if (String.IsNullOrEmpty(brewOrBar))
            {
                brewOrBar = "Both";
            }
            if (String.IsNullOrEmpty(happyHour))
            {
                happyHour = "00:00";
            }
            if (String.IsNullOrEmpty(name))
            {
                name = "";
            }
            string nameFix = name.Replace("%20", " ");
            if (String.IsNullOrEmpty(userLat) || String.IsNullOrEmpty(userLng) || range == null || range == 0)
            {
                userLat = "";
                userLng = "";
                range = 0;
            }

            if(String.IsNullOrEmpty(beerName))
            {
                beerName = "";
            }

            IList<Brewery> breweries = breweryDao.GetAllByQuery(zip, brewOrBar, happyHour, nameFix, userLat, userLng, range, beerName);

            foreach(Brewery brewery in breweries)
            {
                brewery.beersAvailable = beerDao.GetBeerByBrewery(brewery.Id);
            }
            return Ok(breweries);
        }

        [HttpGet("id")]
        [Route("{brewId}")]
        public ActionResult<Brewery> GetBreweryById([FromQuery]int brewId)
        {
            Brewery brewery = breweryDao.GetById(brewId);
            brewery.beersAvailable = beerDao.GetBeerByBrewery(brewId);
            return Ok(brewery);
        }

        [HttpGet("name")]
        [Route("{brewName}")]
        public ActionResult<List<Brewery>> GetBreweriesByName([FromQuery]string brewName)
        {
            IList<Brewery> breweries = breweryDao.GetByName(brewName);
            return Ok(breweries);
        }
    }
}
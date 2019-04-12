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

        public BreweryController(IBreweryDAO breweryDao)
        {
            this.breweryDao = breweryDao;
        }

        [HttpGet]
        public ActionResult<List<Brewery>> GetBreweries([FromQuery]int zip, [FromQuery]string brewOrBar)
        {
            IList<Brewery> breweries = breweryDao.GetAllByZip(zip, brewOrBar);
            return Ok(breweries);
        }

    }
}
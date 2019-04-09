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
    public class BeerController : ControllerBase
    {
        private IBeer beerDao;
        public BeerController(IBeer beerDao)
        {
            this.beerDao = beerDao;
        }

        [HttpGet]
        public ActionResult<List<Beer>> GetBeers([FromQuery]string name,[FromQuery]int style)
        {
            IList<Beer> beers = beerDao.GetBeers(name, style);
            return Ok(beers);
        }
    }
}
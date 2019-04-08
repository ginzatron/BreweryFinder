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
    public class HomeController : ControllerBase
    {
        private IBeer beerDao;
        public HomeController(IBeer beerDao)
        {
            this.beerDao = beerDao;
        }

        [HttpGet]
        public ActionResult<List<Beer>> GetAll()
        {
            IList<Beer> beers = beerDao.GetAll();
            return Ok(beers);
        }
    }
}
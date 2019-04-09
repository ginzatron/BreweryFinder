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

        //[HttpGet]
        //public ActionResult<List<Beer>> GetAll()
        //{
        //    IList<Beer> beers = beerDao.GetAll();
        //    return Ok(beers);
        //}

        [HttpGet]
        public ActionResult<List<Beer>> GetBeers([FromQuery]string name,[FromQuery]int style)
        {

            IList<Beer> beers = beerDao.GetBeers(name, style);
            return Ok(beers);
        }

        //[HttpGet("{id}")]
        //public ActionResult<List<Beer>> GetAllByStyleId(int id)
        //{
        //    IList<Beer> beers = beerDao.GetAllByStyleId(id);
        //    return Ok(beers);
        //}

        //[HttpGet("{name}")]
        //[Route("api/[controller]/getbyname")]
        //public ActionResult<Beer> GetOneBeer(string name)
        //{
        //    Beer beer = beerDao.GetByName(name);
        //    return Ok(beer);
        //}
    }
}
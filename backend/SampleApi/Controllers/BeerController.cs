using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private IBeerDAO beerDao;
        private IFavoriteDAO favoriteDAO;

        public BeerController(IBeerDAO beerDao, IFavoriteDAO favoriteDAO)
        {
            this.beerDao = beerDao;
            this.favoriteDAO = favoriteDAO;
        }

        [HttpGet]
        public ActionResult<List<Beer>> GetBeers([FromQuery]string name,[FromQuery]int styleId)
        {
            IList<Beer> beers = beerDao.GetBeers(name, styleId);
            return Ok(beers);
        }

        [HttpPost]
        [Authorize]
        public ActionResult FavoriteBeer([FromBody]int beerId)
        {
            this.favoriteDAO.AddFavorite(base.User.Identity.Name.ToString(), beerId);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult RemoveFavorite([FromBody]int beerId)
        {
            this.favoriteDAO.RemoveFavorite(base.User.Identity.Name.ToString(), beerId);

            return Ok();
        }
    }
}
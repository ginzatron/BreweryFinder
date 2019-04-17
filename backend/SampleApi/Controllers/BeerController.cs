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
        [Authorize]
        public ActionResult<List<int>> GetFavorites()
        {
            List<int> favorites = favoriteDAO.GetFavorites(base.User.Identity.Name.ToString());
            return Ok(favorites);
        }

        [HttpPost]
        [Authorize]
        public ActionResult FavoriteBeer([FromBody]Beer beer)
        {
            this.favoriteDAO.AddFavorite(base.User.Identity.Name.ToString(), beer.Id);

            return Ok();    
        }

        [HttpDelete]
        [Authorize]
        public ActionResult RemoveFavorite([FromBody]Beer beer)
        {
            this.favoriteDAO.RemoveFavorite(base.User.Identity.Name.ToString(), beer.Id);

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IFavoriteDAO
    {
        bool AddFavorite(string username, int beer_id);
        bool RemoveFavorite(string username, int beer_id);
    }
}

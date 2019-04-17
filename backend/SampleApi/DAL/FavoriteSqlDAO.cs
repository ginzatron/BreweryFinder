using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class FavoriteSqlDAO : IFavoriteDAO
    {
        private string ConnectionString { get; set; }

        public FavoriteSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<int> GetFavorites(string username)
        {
            List<int> favorites = new List<int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select beer_id from favorites where username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        favorites.Add(Convert.ToInt32(reader["beer_id"]));
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return favorites;
        }

        public bool AddFavorite(string username, int beer_id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("insert into favorites values (@beerId, @username)", conn);
                    cmd.Parameters.AddWithValue("@beerId", beer_id);
                    cmd.Parameters.AddWithValue("@username", username);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool RemoveFavorite(string username, int beer_id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("delete from favorites where (beer_id = @beerId and username = @username)", conn);
                    cmd.Parameters.AddWithValue("@beerId", beer_id);
                    cmd.Parameters.AddWithValue("@username", username);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }
}

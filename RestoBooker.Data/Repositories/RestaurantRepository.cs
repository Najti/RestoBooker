using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoBooker.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private string connectionString;
        public RestaurantRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetRestaurantById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> GetRestaurants()
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> GetRestaurantsByFilter(string filter)
        {
            throw new NotImplementedException();
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}

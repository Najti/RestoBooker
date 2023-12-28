using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Interfaces
{
    public interface IRestaurantRepository
    {
        public Restaurant UpdateRestaurant(Restaurant restaurant);
        public List<Restaurant> GetRestaurants();
        public List<Restaurant> GetRestaurantsByFilter(string filter);
        public void DeleteRestaurant(int id);
        public Restaurant GetRestaurantById(int id);
        public Restaurant AddRestaurant(Restaurant restaurant);
    }
}

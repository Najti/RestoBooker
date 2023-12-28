using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Services
{
    public class RestaurantService
    {
        private IRestaurantRepository repo;
        public RestaurantService(IRestaurantRepository repo)
        {
            this.repo = repo;
        }
        public Restaurant UpdateRestaurant(Restaurant restaurant) { return repo.UpdateRestaurant(restaurant); }
        public List<Restaurant> GetRestaurants() { return repo.GetRestaurants(); }
        public List<Restaurant> GetRestaurantsByFilter(string filter) { return repo.GetRestaurantsByFilter(filter); }
        public void DeleteRestaurant(int id) { repo.DeleteRestaurant(id); }
        public Restaurant GetRestaurantById(int id) { return repo.GetRestaurantById(id); }
        public Restaurant AddRestaurant(Restaurant restaurant) { return repo.AddRestaurant(restaurant); }
    }
}

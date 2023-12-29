using Microsoft.AspNetCore.Mvc;
using RestoBooker.API.Mappers;
using RestoBooker.API.Model.Input;
using Restobooker.Domain.Model;
using Restobooker.Domain.Services;

namespace RestoBooker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private RestaurantService restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpGet("{RestaurantId}")]
        public ActionResult<Restaurant> GetRestaurantByID(int RestaurantId)
        {
            try
            {
                Restaurant u = restaurantService.GetRestaurantById(RestaurantId);

                if (u != null)
                {
                    return Ok(u.Name);
                }
                else
                {
                    return NotFound("Restaurant not found"); // Adjust message to indicate restaurant not found
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Change to return BadRequest for validation errors
            }
        }

        [HttpPost]
        public ActionResult<Restaurant> PostRestaurant([FromBody] RestaurantRestInputDTO restDTO)
        {
            try
            {
                Restaurant restaurant = restaurantService.AddRestaurant(MapToDomain.MapToRestaurantDomain(restDTO));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Restaurant> UpdateRestaurant(int id, [FromBody] RestaurantRestInputDTO restDTO)
        {
            try
            {
                if (id != MapToDomain.MapToRestaurantDomain(restDTO).RestaurantId) return BadRequest();
                restaurantService.UpdateRestaurant(MapToDomain.MapToRestaurantDomain(restDTO));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            try
            {
                restaurantService.DeleteRestaurant(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public ActionResult<List<Restaurant>> GetAllRestaurants()
        {
            try
            {
                List<Restaurant> restaurants = restaurantService.GetRestaurants();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<Restaurant>> GetRestaurantsByFilter([FromQuery] string filter)
        {
            try
            {
                List<Restaurant> restaurants = restaurantService.GetRestaurantsByFilter(filter);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Restobooker.Domain.Model;
using Restobooker.Domain.Services;
using RestoBooker.API.Mappers;
using RestoBooker.API.Model.Input;
using System;

namespace RestoBooker.API.Controllers
{
    [Route("api/[controller]/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{UserId}")]
        public ActionResult<User> GetUserByID(int UserId)
        {
            try
            {
                User u = userService.GetUserById(UserId);

                if (u != null)
                {
                    return Ok(u.Name);
                }
                else
                {
                    return NotFound("User not found"); // Adjust message to indicate user not found
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Change to return BadRequest for validation errors
            }
        }
        [HttpPost]
        public ActionResult<User> PostUser([FromBody] UserRestInputDTO restDTO)
        {
            try
            {
                User user = userService.AddUser(MapToDomain.MapToUserDomain(restDTO));
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        //public User mapUser(UserRestInputDTO restDTO)
        //{

        //}
    }
}

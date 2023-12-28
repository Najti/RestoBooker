using Microsoft.AspNetCore.Mvc;
using Restobooker.Domain.Model;
using Restobooker.Domain.Services;
using RestoBooker.API.Mappers;
using RestoBooker.API.Model.Input;
using System;
using System.Collections.Generic;

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] UserRestInputDTO restDTO)
        {
            try
            {
                if (id != MapToDomain.MapToUserDomain(restDTO).CustomerId) return BadRequest();
                userService.UpdateUser(MapToDomain.MapToUserDomain(restDTO));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsersByFilter")]
        public ActionResult<List<User>> GetUsersByFilter(string filter)
        {
            try
            {
                List<User> users = userService.GetUsersByFilter(filter);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public ActionResult<List<User>> GetDeletedUsers()
        {
            try
            {
                List<User> deletedUsers = userService.GetDeletedUsers();
                return Ok(deletedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

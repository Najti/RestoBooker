using Microsoft.AspNetCore.Mvc;
using RestoBooker.API.Mappers;
using RestoBooker.API.Model.Input;
using Restobooker.Domain.Model;
using Restobooker.Domain.Services;

namespace RestoBooker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private ReservationService reservationService;

        public ReservationController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("{ReservationId}")]
        public ActionResult<Reservation> GetReservationByID(int ReservationId)
        {
            try
            {
                Reservation u = reservationService.GetReservationById(ReservationId);

                if (u != null)
                {
                    return Ok(u);
                }
                else
                {
                    return NotFound("Reservation not found"); // Adjust message to indicate reservation not found
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Change to return BadRequest for validation errors
            }
        }
        [HttpGet("/User/{UserID}")]
        public ActionResult<Reservation> GetReservationByUserID(int UserID)
        {
            try
            {
                Reservation u = reservationService.GetReservationById(UserID);

                if (u != null)
                {
                    return Ok(u);
                }
                else
                {
                    return NotFound("Reservation not found"); // Adjust message to indicate reservation not found
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Change to return BadRequest for validation errors
            }
        }


        [HttpPost]
        public ActionResult<Reservation> PostReservation([FromBody] ReservationRestInputDTO restDTO)
        {
            try
            {
                Reservation reservation = reservationService.AddReservation(MapToDomain.MapToReservationDomain(restDTO));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Reservation> UpdateReservation(int id, [FromBody] ReservationRestInputDTO restDTO)
        {
            try
            {
                if (id != MapToDomain.MapToReservationDomain(restDTO).ReservationNumber) return BadRequest();
                reservationService.UpdateReservation(MapToDomain.MapToReservationDomain(restDTO));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                reservationService.DeleteReservation(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public ActionResult<List<Reservation>> GetAllReservations()
        {
            try
            {
                List<Reservation> reservations = reservationService.GetReservations();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<Reservation>> GetReservationsByFilter([FromQuery] string filter)
        {
            try
            {
                List<Reservation> reservations = reservationService.GetReservationsByFilter(filter);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models.DTOs;
using Restaurant.Services;
using Restaurant.Services.IServices;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync();
                return Ok(reservations);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving reservations.");
            };
        }

        // GET: api/Reservation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationsByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return Ok(reservation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the reservation.");
            }
        }

        // POST: api/Reservation
        [HttpPost("AddReservation")]

        public async Task<ActionResult> AddReservation([FromBody] ReservationDTO reservationDTO)
        {
            try
            {
                await _reservationService.AddReservationsAsync(reservationDTO);
                return CreatedAtAction(nameof(GetReservationById), new { id = reservationDTO.ReservationId }, reservationDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {               
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the reservation.");
            }
        }

        // PUT: api/Reservation/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] ReservationDTO reservationDTO)
        {
            if (id != reservationDTO.ReservationId)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            try
            {
                await _reservationService.UpdateReservationsAsync(reservationDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the reservation.");
            }
        }

        // DELETE: api/Reservation/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            try
            {
                var deleted = await _reservationService.DeleteReservationsAsync(id);
                if (deleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the reservation.");
            }
        }
        // GET: api/Reservation/by-date
        [HttpGet("by-date")]
        public async Task<IActionResult> GetReservationsByDate([FromQuery] DateTime date, [FromQuery] TimeOnly time)
        {
            try
            {
                var reservations = await _reservationService.GetReservationByDatesAsync(date, time);
                if (reservations == null || !reservations.Any())
                {
                    return NotFound();
                }
                return Ok(reservations);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving reservations.");
            }
        }
      



    }
}

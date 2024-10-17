using Microsoft.AspNetCore.Mvc;
using PersonalTrainerAPI.Models;

namespace PersonalTrainerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private static List<Booking> Bookings = new List<Booking>();

        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Booking details are missing.");
            }

            // Add booking to the list (in-memory for now, we’ll persist to a database later)
            booking.Id = Bookings.Count + 1;
            Bookings.Add(booking);

            return Ok(new { message = "Booking created successfully", booking });
        }
    }
}

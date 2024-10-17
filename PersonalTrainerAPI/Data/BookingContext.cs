using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerAPI.Models;

namespace PersonalTrainerAPI.Data
{
    public class BookingContext : IdentityDbContext<ApplicationUser>
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}

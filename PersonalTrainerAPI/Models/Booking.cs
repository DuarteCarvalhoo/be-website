using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalTrainerAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Service { get; set; }
        
        [Required]
        public DateTime PreferredDate { get; set; }
        
        [Required]
        public string PreferredTime { get; set; }
    }
}
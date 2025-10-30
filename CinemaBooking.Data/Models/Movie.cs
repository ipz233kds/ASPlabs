using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CinemaBooking.Data.Models
{
    public class Movie
    {
        public long MovieID { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву фільму")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть жанр")]
        public string Genre { get; set; } = string.Empty;
        public ICollection<MovieSession>? Sessions { get; set; }
    }
}
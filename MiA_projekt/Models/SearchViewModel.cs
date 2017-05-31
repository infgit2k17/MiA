using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(1)]
        public string Destination { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        [Range(1, 1000)]
        public int Guests { get; set; }
    }
}
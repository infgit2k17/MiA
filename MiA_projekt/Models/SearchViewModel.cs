using MiA_projekt.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(1)]
        public string Destination { get; set; }

        [FutureDate]
        public DateTime Arrival { get; set; }

        [FutureDate]
        public DateTime Departure { get; set; }

        [Range(1, 1000)]
        public int Guests { get; set; }
    }
}
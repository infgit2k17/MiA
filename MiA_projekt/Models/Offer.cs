using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Offer // co ze zdjęciami?
    {
        [Key]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public string GuestId { get; set; }

        public AppUser Guest { get; set; }

        [Range(1, 1000)]
        public int GuestCount { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [Range(0, 5)]
        public short Rate { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        public string HostId { get; set; }

        public AppUser Host { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public short GuestCount { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int RatePoints { get; set; }

        public int RatesCount { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        // HostId

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        public int Addressid { get; set; }

        public Address Address { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        // PhotoId

        [Range(1, 1000)]
        public short GuestCount { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
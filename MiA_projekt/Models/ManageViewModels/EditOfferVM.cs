using MiA_projekt.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.ManageViewModels
{
    public class EditOfferVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Guest Count")]
        public short GuestCount { get; set; }

        [Required]
        [FutureDate]
        public DateTime From { get; set; }

        [Required]
        [FutureDate]
        public DateTime To { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string CountryCode { get; set; }
    }
}

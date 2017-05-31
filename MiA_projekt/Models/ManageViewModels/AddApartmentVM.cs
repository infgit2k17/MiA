using MiA_projekt.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.ManageViewModels
{
    public class AddApartmentVM
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Image")]
        public IFormFile ImageFile { get; set; }

        [Required]
        [Display(Name = "Guest Count")]
        public short GuestCount { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3)]
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayName("Country")]
        public string CountryCode { get; set; }
    }
}

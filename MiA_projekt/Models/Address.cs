using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

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

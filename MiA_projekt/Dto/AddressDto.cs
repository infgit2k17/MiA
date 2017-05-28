using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Dto
{
    public class AddressDto
    {
        [OnClick("readData(\" + item.id + \")", "item.id")]
        public int Id { get; set; }

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
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        [DisplayName("Country code")]
        public string CountryCode { get; set; }

    }
}

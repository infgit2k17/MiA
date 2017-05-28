using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Dto
{
    public class ApartmentDto
    {
        [OnClick("readData(\" + item.id + \")", "item.id")]
        public int Id { get; set; }

        public string HostId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        [DisplayName("Address Id")]
        public int AddressId { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Range(1, 1000)]
        [DisplayName("Guests")]
        public short GuestCount { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        [DisplayName("Rate points")]
        public int RatePoints { get; set; }

        [DisplayName("Rates count")]
        public int RatesCount { get; set; }
    }
}

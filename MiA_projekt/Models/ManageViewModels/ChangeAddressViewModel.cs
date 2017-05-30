using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.ManageViewModels
{
    public class ChangeAddressViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "This field is required")]
        public string Street { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "This field is required")]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "This field is required")]
        public string PostalCode { get; set; }

        [StringLength(100, ErrorMessage = "This field is required")]
        public string CountryCode { get; set; }
    }
}

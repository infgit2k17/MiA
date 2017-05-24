using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.Models.ManageViewModels
{
    public class ChangeAddressViewModel
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayName("Country")]
        public string CountryCode { get; set; }
    }
}

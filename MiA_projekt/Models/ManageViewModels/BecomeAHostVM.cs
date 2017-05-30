using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.Models.ManageViewModels
{
    public class BecomeAHostVM
    {
        [Required]
        public int Pesel { get; set; }

        [Required]
        [DisplayName("ID number")]
        public int IDnumber { get; set; }

        [Required]
        [DisplayName("Photo ID card")]
        public IFormFile file { get; set; }

    }
}

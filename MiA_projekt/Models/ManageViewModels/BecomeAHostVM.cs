using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public IFormFile file { get; set; }

    }
}

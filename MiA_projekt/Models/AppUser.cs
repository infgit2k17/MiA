using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string Surname { get; set; }

        public bool Sex { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        // PhotoId

        public decimal Balance { get; set; }
    }
}
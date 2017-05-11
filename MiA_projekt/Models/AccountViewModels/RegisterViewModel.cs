using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The field can not be empty.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The field can not be empty.", MinimumLength = 6)]
        public string Surname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }

        public bool Sex { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Enter correct number.", MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

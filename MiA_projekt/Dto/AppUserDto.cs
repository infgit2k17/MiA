using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Dto
{
    public class AppUserDto
    {
        [OnClick("readData('\" + item.id + \"')", "item.id")]
        [Orderable(false)]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(maximumLength: 20)]
        [Required]
        public string Name { get; set; }

        [StringLength(maximumLength: 20)]
        [Required]
        public string Surname { get; set; }

        public decimal Balance { get; set; }

        [DisplayName("Email confirmed")]
        [Searchable(false)]
        [Orderable(false)]
        public bool EmailConfirmed { get; set; }
    }
}

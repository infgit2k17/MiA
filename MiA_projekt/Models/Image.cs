using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Url { get; set; }
    }
}
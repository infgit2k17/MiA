using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }
    }
}

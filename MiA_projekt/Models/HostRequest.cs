using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models
{
    public class HostRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string PersonalId { get; set; }

        [Required]
        public string DocumentId { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        public DateTime Date { get; set; }

        public bool IsRejected { get; set; }
    }
}

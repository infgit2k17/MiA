using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.ManageViewModels
{
    public class BecomeAHostVM
    {
        [Required]
        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [Required]
        [DisplayName("Document ID")]
        public string DocumentId { get; set; }

        [Required]
        [DisplayName("Document photo")]
        public IFormFile File { get; set; }
    }
}
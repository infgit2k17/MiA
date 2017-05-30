using System.ComponentModel;

namespace MiA_projekt.Models.ManageViewModels
{
    public class HostRequestsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [DisplayName("Document ID")]
        public string DocumentId { get; set; }

        [DisplayName("Document photo")]
        public string Image { get; set; }
    }
}
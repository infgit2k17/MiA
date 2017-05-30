using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        public IFormFile File { get; set; }

    }
}

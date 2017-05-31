using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Models.AccountViewModels
{
    public class MyOrderVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string HostName { get; set; }

        public string HostSurname { get; set; }

        public string PhoneNumber { get; set; }

        public string Comment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.Models.AccountViewModels
{
    public class MyOrdersVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

    }
}

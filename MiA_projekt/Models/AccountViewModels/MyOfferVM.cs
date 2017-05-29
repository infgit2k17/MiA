namespace MiA_projekt.Models.AccountViewModels
{
    public class MyOfferVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Address Address { get; set; }

        public decimal Price { get; set; }

        public short GuestCount { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}

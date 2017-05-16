using MiA_projekt.Data;
using MiA_projekt.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiA_projekt.Manager
{
    public class ApartmentManager
    {
        private readonly AppDbContext _db;

        public ApartmentManager(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ApartmentViewModel> FindOffers(SearchViewModel vm)
        {
            return _db.Apartments.Where(o => o.GuestCount == vm.Guests &&
                                  o.Address.City.ToLower() == vm.Destination &&
                                  o.From <= vm.Arrival &&
                                  o.To >= vm.Departure)
                                  .Select(o =>
                                            new ApartmentViewModel
                                            {
                                                ImageUrl = "TO DO",
                                                Price = o.Price,
                                                RatingStars = CalculateRates(o.RatePoints, o.RatesCount),
                                                Title = o.Name
                                            })
                                  .ToList();
        }

        private double CalculateRates(int ratePoints, int rateCount)
        {
            if (rateCount == 0)
                return 0d;

            return ratePoints / (double)rateCount;
        }
    }
}

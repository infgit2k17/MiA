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

        public IEnumerable<ApartmentVM> FindApartments(SearchViewModel vm)
        {
            return _db.Apartments.Where(o => o.GuestCount == vm.Guests &&
                                             o.Address.City.ToLower().Contains(vm.Destination.ToLower()) &&
                                             o.From <= vm.Arrival &&
                                             o.To >= vm.Departure)
                                .Select(o =>
                                    new ApartmentVM
                                    {
                                        Id = o.Id,
                                        ImageUrl = o.Image,
                                        Price = o.Price,
                                        RatingStars = CalculateRates(o.RatePoints, o.RatesCount),
                                        Title = o.Name
                                    });
        }

        public ApartmentDetailsVM GetAparmentDetails(int id)
        {
            Apartment a = _db.Apartments.Find(id);
            return new ApartmentDetailsVM
            {
                Id = a.Id,
                Images = GetImages(a.Id),
                Price = a.Price,
                RatingStars = CalculateRates(a.RatePoints, a.RatesCount),
                Title = a.Name,
                Description = a.Description
            };
        }

        private IEnumerable<string> GetImages(int apartmentId)
        {
            return _db.Images.Where(i => i.ApartmentId == apartmentId)
                             .Select(i => i.Url);
        }

        private double CalculateRates(int ratePoints, int rateCount)
        {
            if (rateCount == 0)
                return 0d;

            return ratePoints / (double)rateCount;
        }
    }
}

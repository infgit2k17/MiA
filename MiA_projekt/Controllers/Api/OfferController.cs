using MiA_projekt.Data;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    public class OfferController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public OfferController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/offers/{id}")]
        public IActionResult Book(int id)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var apartment = _db.Apartments.Find(id);

            _db.Offers.Add(new Offer
            {
                GuestId = userId,
                ApartmentId = apartment.Id,
                From = apartment.From,
                To = apartment.To,
                GuestCount = apartment.GuestCount,
            });

            _db.SaveChanges();

            return Ok();
        }
    }
}
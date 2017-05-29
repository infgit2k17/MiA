using AutoMapper;
using MiA_projekt.Data;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    public class ApartmentsUserController : ApiController
    {
        public ApartmentsUserController(AppDbContext db, IMapper mapper) : base(db, mapper)
        { }

        [HttpDelete]
        [Route("api/apartments-user/{id}")]
        public IActionResult Remove(int id)
        {
            return RemoveItem<Apartment>(id);
        }
    }
}
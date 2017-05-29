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

        [HttpPut]
        [Route("api/apartments-user/{id}")]
        public IActionResult Update(int id, ApartmentVM dto)
        {
            return UpdateItem<Apartment, ApartmentVM>(id, dto);
        }

        [HttpDelete]
        [Route("api/apartments-user/{id}")]
        public IActionResult Remove(int id)
        {
            return RemoveItem<Apartment>(id);
        }
    }
}
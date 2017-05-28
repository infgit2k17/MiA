using AutoMapper;
using MiA_projekt.Data;
using MiA_projekt.Dto;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class ApartmentsController : ApiController
    {
        public ApartmentsController(AppDbContext db, IMapper mapper) : base(db, mapper)
        { }

        [HttpPost]
        [Route("api/apartments")]
        public IActionResult Create(ApartmentDto dto)
        {
            return AddItem<Apartment, ApartmentDto>(dto);
        }

        [HttpGet]
        [Route("api/apartments/{id}")]
        public IActionResult Get(int id)
        {
            return GetItem<Apartment, ApartmentDto>(id);
        }

        [HttpPost]
        [Route("api/datatable/apartments")]
        public IActionResult Get(DataTableParamDto dto)
        {
            return GetItems<Apartment, ApartmentDto>(dto);
        }

        [HttpPut]
        [Route("api/apartments/{id}")]
        public IActionResult Update(int id, ApartmentDto dto)
        {
            return UpdateItem<Apartment, ApartmentDto>(id, dto);
        }

        [HttpDelete]
        [Route("api/apartments/{id}")]
        public IActionResult Remove(int id)
        {
            return RemoveItem<Apartment>(id);
        }
    }
}
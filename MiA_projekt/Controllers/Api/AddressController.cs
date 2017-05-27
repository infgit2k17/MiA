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
    public class AddressController : ApiController
    {
        public AddressController(AppDbContext db, IMapper mapper) : base(db, mapper)
        { }

        [HttpPost]
        [Route("api/addresses")]
        public IActionResult Create(AddressDto dto)
        {
            return AddItem<Address, AddressDto>(dto);
        }

        [HttpGet]
        [Route("api/addresses/{id}")]
        public IActionResult Get(int id)
        {
            return GetItem<Address, AddressDto>(id);
        }

        [HttpPost]
        [Route("api/datatable/addresses")]
        public IActionResult Get(DataTableParamDto dto)
        {
            return GetItems<Address, AddressDto>(dto);
        }

        [HttpPut]
        [Route("api/addresses/{id}")]
        public IActionResult Update(int id, AddressDto dto)
        {
            return UpdateItem<Address, AddressDto>(id, dto);
        }

        [HttpDelete]
        [Route("api/addresses/{id}")]
        public IActionResult Remove(int id)
        {
            return RemoveItem<Address>(id);
        }
    }
}
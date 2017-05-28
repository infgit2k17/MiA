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
    public class UserController : ApiController
    {
        public UserController(AppDbContext db, IMapper mapper) : base(db, mapper)
        { }

        [HttpPost]
        [Route("api/users")]
        public IActionResult Create(AppUserDto dto)
        {
            return AddItem<AppUser, AppUserDto>(dto);
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public IActionResult Get(string id)
        {
            return GetItem<AppUser, AppUserDto>(id);
        }

        [HttpPost]
        [Route("api/datatable/users")]
        public IActionResult Get(DataTableParamDto dto)
        {
            return GetItems<AppUser, AppUserDto>(dto);
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public IActionResult Update(string id, AppUserDto dto)
        {
            return UpdateItem<AppUser, AppUserDto>(id, dto);
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public IActionResult Remove(string id)
        {
            return RemoveItem<AppUser>(id);
        }
    }
}
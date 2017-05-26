using MiA_projekt.Data;
using MiA_projekt.Dto;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/users")]
        public IActionResult Create(AppUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _db.Users.Add(new AppUser
            {
                Email = dto.Email,
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                Balance = dto.Balance,
                EmailConfirmed = dto.EmailConfirmed
            });
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public IActionResult Get(string id)
        {
            return Ok(UserToDto(_db.Users.Find(id)));
        }

        [HttpPost]
        [Route("api/datatable/users")]
        public IActionResult Get(DataTableParamDto dto)
        {
            int count = _db.Users.Count();

            return Ok(new DataTableDto<AppUserDto>
            {
                Draw = dto.Draw,
                RecordsFiltered = count,
                RecordsTotal = count,
                Data = _db.Users.ToList().Select(UserToDto)
            });
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public IActionResult Update(string id, AppUserDto dto)
        {
            AppUser user = _db.Users.Find(id);
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Balance = dto.Balance;
            user.EmailConfirmed = dto.EmailConfirmed;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public IActionResult Remove(string id)
        {
            AppUser user = _db.Users.Find(id);
            _db.Users.Remove(user);
            _db.SaveChanges();

            return Ok();
        }

        private AppUserDto UserToDto(AppUser user)
        {
            return new AppUserDto
            {
                Balance = user.Balance,
                Email = user.Email, 
                EmailConfirmed = user.EmailConfirmed,
                Name = user.Name,
                Surname = user.Surname,
                Id = user.Id
            };
        }
    }
}
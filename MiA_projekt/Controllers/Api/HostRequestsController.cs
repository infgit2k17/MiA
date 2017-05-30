using MiA_projekt.Data;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin,Mod")]
    public class HostRequestsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;

        public HostRequestsController(UserManager<AppUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpPost]
        [Route("api/hostrequests/{id}")]
        public async Task<IActionResult> Accept(int id)
        {
            var request = _db.HostRequests.Include(i => i.User).FirstOrDefault(i => i.Id == id);
            await _userManager.AddToRoleAsync(request.User, "Host");
            _db.HostRequests.Remove(request);
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/hostrequests/{id}")]
        public IActionResult Reject(int id)
        {
            var request = _db.HostRequests.FirstOrDefault(i => i.Id == id);
            request.IsRejected = true;
            _db.SaveChanges();

            return Ok();
        }
    }
}
using MiA_projekt.Dto;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MiA_projekt.Controllers
{
    [Authorize(Roles = "Admin,Mod")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public ViewResult Addresses()
        {
            return EditorWithTable<AddressDto>("Addresses", "addresses");
        }

        public async Task<ViewResult> Apartments()
        {
            await IsMod();
            return EditorWithTable<ApartmentDto>("Apartments", "apartments");
        }

        public async Task<ViewResult> Comments()
        {
            await IsMod();
            return EditorWithTable<CommentDto>("Comments", "comments");
        }

        public async Task<ViewResult> Users()
        {
            await IsMod();
            return EditorWithTable<AppUserDto>("Users", "users");
        }

        private ViewResult EditorWithTable<T>(string title, string apiActionName) where T : class
        {
            ViewData["Title"] = title;
            ViewBag.ApiUrl = "/api/" + apiActionName + "/";
            ViewBag.DataTable = new DataTableHtmlHelper<T>
            {
                Buttons = new[]
                {
                    new Button
                    {
                        Name = "Remove",
                        ActionUri = ViewBag.ApiUrl,
                        ConfirmMsg = "Are you sure you want to remove",
                        DataId = "id",
                        AddAntiForgeryToken = false
                    }
                },
                DataSource = "/api/datatable/" + apiActionName + "/"
            };
            ViewBag.EditorForm = new EditorFormHtmlHelper<T>();

            return View("EditorWithTable");
        }

        private async Task IsMod()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (await _userManager.IsInRoleAsync(user, "Mod"))
                ViewBag.Editor = false;
        }

        public IActionResult HostRequests()
        {
            //todog
            return View();
        }
    }
}
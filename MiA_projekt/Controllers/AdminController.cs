using MiA_projekt.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ViewResult Users()
        {
            return EditorWithTable<AppUserDto>("Users", "users");
        }

        public ViewResult Addresses()
        {
            return EditorWithTable<AddressDto>("Addresses", "addresses");
        }

        public ViewResult Apartments()
        {
            return EditorWithTable<ApartmentDto>("Apartments", "apartments");
        }

        public ViewResult Comments()
        {
            return EditorWithTable<CommentDto>("Comments", "comments");
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

    }
}
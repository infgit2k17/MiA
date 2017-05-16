using MiA_projekt.Data;
using MiA_projekt.Manager;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db;
        private readonly ApartmentManager _manager;

        public HomeController(AppDbContext db)
        {
            _db = db;
            _manager = new ApartmentManager(db);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(SearchViewModel vm)
        {
            if (vm == null || !ModelState.IsValid)
                return Error("Please specify search parameters.");

            return View(_manager.FindOffers(vm));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error(string message = "")
        {
            return View("Error", message);
        }

        public IActionResult Description()
        {
            return View();
        }
    }
}

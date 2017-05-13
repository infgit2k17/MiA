using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            ViewData["hotelResults"] = 800; // liczba hoteli dla konkretnego wyszukania
            return View();
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

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Description()
        {
            String[] tab = { "ala", "ola", "ela", "marysia" };
            ViewData["Description"] = "Opis apartamentu";
            ViewData["Comments"] = tab;

            return View();
        }
    }
}

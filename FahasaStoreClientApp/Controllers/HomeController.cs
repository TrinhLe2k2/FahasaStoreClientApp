using FahasaStoreClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FahasaStoreClientApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult filter()
        {
            return View();
        }
        public IActionResult Banner()
        {
            return View();
        }
    }
}

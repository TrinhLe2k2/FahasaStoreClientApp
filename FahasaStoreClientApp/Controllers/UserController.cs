using FahasaStoreClientApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class UserController : Controller
    {

        public UserController()
        {
            
        }

        public IActionResult ControlPanel()
        {
            return View();
        }

        public IActionResult Information()
        {
            return View();
        }

        public IActionResult Pay()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Vouchers()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult OrderDetail()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult ReviewsUpdate()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class UserController : Controller
    {
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
    }
}

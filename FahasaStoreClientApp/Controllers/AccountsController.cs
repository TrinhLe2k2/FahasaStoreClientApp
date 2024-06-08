using FahasaStoreClientApp.Models.CustomModel;
using FahasaStoreClientApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IJwtTokenDecoder _jwtTokenDecoder;

        public AccountsController(IAccountService accountService, IJwtTokenDecoder jwtTokenDecoder)
        {
            _accountService = accountService;
            _jwtTokenDecoder = jwtTokenDecoder;
        }

        public IActionResult Account()
        {
            return PartialView();
        }

        //public IActionResult Register()
        //{
        //    return PartialView();
        //}
        //public IActionResult Login()
        //{
        //    return PartialView();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                return RedirectToAction("Index", "Home");
            }

            bool isRegistered = await _accountService.RegisterAsync(model);
            if (isRegistered)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "login failed. Please try again.");
                return RedirectToAction("Index", "Home");
            }
            var accessToken = await _accountService.LogInAsync(model);
            var userClaims = _jwtTokenDecoder.DecodeToken(accessToken).Claims;
            var UserId = userClaims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var FullName = userClaims.FirstOrDefault(c => c.Type == "FullName")?.Value;
            var Avatar = userClaims.FirstOrDefault(c => c.Type == "Avatar")?.Value;

            HttpContext.Session.SetString("JWToken", accessToken);
            HttpContext.Session.SetString("UserId", UserId ?? string.Empty);
            HttpContext.Session.SetString("FullName", FullName ?? string.Empty);
            HttpContext.Session.SetString("Avatar", Avatar ?? string.Empty);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            if (accessToken == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy phiên đăng nhập.";
                return RedirectToAction("Index", "Home");
            }
            var result = await _accountService.LogOutAsync(accessToken);

            HttpContext.Session.Remove("JWToken");
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("Avatar");

            TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}

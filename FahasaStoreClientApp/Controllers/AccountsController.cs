using FahasaStoreClientApp.Helpers;
using FahasaStoreClientApp.Models;
using FahasaStoreClientApp.Models.CustomModel;
using FahasaStoreClientApp.Services;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserLogined _userLogined;
        private readonly IUserService _userService;
        private readonly IJwtTokenDecoder _jwtTokenDecoder;

        public AccountsController(IAccountService accountService, UserLogined userLogined, IUserService userService, IJwtTokenDecoder jwtTokenDecoder)
        {
            _accountService = accountService;
            _userLogined = userLogined;
            _userService = userService;
            _jwtTokenDecoder = jwtTokenDecoder;
        }

        public IActionResult Account()
        {
            return PartialView();
        }

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

            if (UserId == null) { return RedirectToAction("LogOut"); }

            var currentUser = await _userService.GetByIdAsync(UserId);
            _userLogined.CurrentUser = currentUser;
            _userLogined.JWToken = accessToken;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            if (_userLogined.JWToken != null)
            {
                var result = await _accountService.LogOutAsync(_userLogined.JWToken);
                HttpContext.Session.Clear();
            }
            TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}

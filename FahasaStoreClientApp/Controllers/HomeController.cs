using FahasaStoreClientApp.DataTemp.DataVM;
using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Helpers;
using FahasaStoreClientApp.Services;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly UserLogined _userLogined;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly INotificationService _notificationService;
        private readonly ICategoryService _categoryService;
        private readonly IBannerService _bannerService;
        private readonly IMenuService _menuService;
        private readonly IFlashSaleService _flashSaleService;

        public HomeController(
            IHomeService homeService,
            IUserService userService, 
            ICartItemService cartItemService, 
            UserLogined userLogined, 
            INotificationService notificationService, 
            ICategoryService categoryService,
            IBannerService bannerService,
            IMenuService menuService,
            IFlashSaleService flashSaleService
            )
        {
            _homeService = homeService;
            _userService = userService;
            _cartItemService = cartItemService;
            _userLogined = userLogined;
            _notificationService = notificationService;
            _categoryService = categoryService;
            _bannerService = bannerService;
            _menuService = menuService;
            _flashSaleService = flashSaleService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Banners"] = await _bannerService.GetPaginationAsync(1, 12);

            ViewData["Menus"] = await _menuService.GetPaginationAsync(1, 20);

            //ViewData["FlashSale"] = await _flashSaleService.;

            ViewData["TrendOfDay"] = new BookData().ListLimitedBooks(10);
            ViewData["TrendOfMonth"] = new BookData().ListLimitedBooks(10).OrderByDescending(e=>e.BookId).ToList();
            ViewData["TrendOfYear"] = new BookData().ListLimitedBooks(10);

            ViewData["TopSellingBooksDefault"] = new BookData().ListLimitedBooks(5);

            ViewData["TrademarkProductDefault"] = new BookData().ListLimitedBooks(10);

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
        public IActionResult Banner(int id)
        {
            var banners = new BannerData();
            var banner = banners.Banner(id);
            if (banner != null)
            {
                return View(banner);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetCart()
        {
            var user = await _userService.GetByIdAsync(_userLogined.UserId);
            var cartId = user.Cart != null ? user.Cart.Id.ToString() : string.Empty;
            return PartialView(await _cartItemService.GetListByAsync("CartId", cartId));
        }
        public async Task<IActionResult> GetNotifications()
        {
            return PartialView(await _notificationService.GetListByAsync("UserId", _userLogined.UserId));
        }
        public async Task<IActionResult> GetCategories()
        {
            return PartialView(await _categoryService.GetAllAsync());
        }
    }
}

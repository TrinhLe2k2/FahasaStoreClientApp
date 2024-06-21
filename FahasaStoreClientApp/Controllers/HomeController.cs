using AutoMapper;
using FahasaStoreClientApp.DataTemp.DataVM;
using FahasaStoreClientApp.Helpers;
using FahasaStoreClientApp.Models.CustomModels;
using FahasaStoreClientApp.Models.DTO;
using FahasaStoreClientApp.Services;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;


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
        private readonly IFlashSaleBookService _flashSaleBookService;
        private readonly IPartnerService _partnerService;
        private readonly IVoucherService _voucherService;
        private readonly IReviewService _reviewService;
        private readonly IPartnerTypeService _partnerTypeService;
        private readonly IAuthorService _authorService;
        private readonly ICoverTypeService _coverTypeService;


        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public HomeController(
            IHomeService homeService,
            IUserService userService, 
            ICartItemService cartItemService, 
            UserLogined userLogined, 
            INotificationService notificationService, 
            ICategoryService categoryService,
            IBannerService bannerService,
            IMenuService menuService,
            IFlashSaleService flashSaleService,
            IFlashSaleBookService flashSaleBookService,
            IPartnerService partnerService,
            IVoucherService voucherService,
            IReviewService reviewService,
            IMapper mapper,
            IBookService bookService,
            IPartnerTypeService partnerTypeService,
            IAuthorService authorService,
            ICoverTypeService coverTypeService
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
            _flashSaleBookService = flashSaleBookService;
            _partnerService = partnerService;
            _voucherService = voucherService;
            _reviewService = reviewService;
            _mapper = mapper;
            _bookService = bookService;
            _partnerTypeService = partnerTypeService;
            _authorService = authorService;
            _coverTypeService = coverTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var bannerPage = await _bannerService.GetPagination(1, 12);
            ViewData["Banners"] = bannerPage.Items;
            var menuPage = await _menuService.GetPagination(1, 20);
            ViewData["Menus"] = menuPage.Items;
            var flashsalebooks = await _flashSaleBookService.GetFlashSaleBooksToday(1, 20);
            ViewData["FlashSale"] = flashsalebooks.Items;

            var trendOfMonth = await _bookService.GetMonthlyTrendingBooks();
            var trendOfYear = await _bookService.GetYearlyTrendingBooks();
            ViewData["TrendOfMonth"] = trendOfMonth.Items;
            ViewData["TrendOfYear"] = trendOfYear.Items;

            var topSellingBooksDefault = await _bookService.GetTopSellingBooksByCategory(1, 1, 5);
            ViewData["TopSellingBooksDefault"] = topSellingBooksDefault.Items;

            var categories = await _categoryService.GetPagination(1, 3);
            ViewData["categories"] = categories.Items;

            var partners = await _partnerService.GetPagination(1, 100);
            ViewData["Partners"] = partners.Items;

            if(_userLogined.CurrentUser != null && _userLogined.CurrentUser.Cart != null)
            {
                var pageFindSimilarBooksBasedOnCart = await _bookService.FindSimilarBooksBasedOnCart(_userLogined.CurrentUser.Cart.Id);
                ViewData["pageFindSimilarBooksBasedOnCart"] = pageFindSimilarBooksBasedOnCart;
            }

            return View();
        }
        public async Task<IActionResult> Product(int id)
        {
            var vouchers = await _voucherService.GetAllAsync();
            ViewData["Vouchers"] = vouchers;

            var similarBooks = await _bookService.SimilarBooks(id, 20);
            ViewData["SimilarBooks"] = similarBooks;

            var pageReview = await _reviewService.GetListByAsync("BookId", id.ToString());
            ViewData["Reviews"] = pageReview.Items;

            if (_userLogined.CurrentUser != null && _userLogined.CurrentUser.Cart != null)
            {
                var hasOrder = await _bookService.HasUserPurchasedBook(_userLogined.CurrentUser.Id, id);
                ViewData["HasOrder"] = hasOrder;

                var pageFindSimilarBooksBasedOnCart = await _bookService.FindSimilarBooksBasedOnCart(_userLogined.CurrentUser.Cart.Id, aggregationMethod : "minking");
                ViewData["pageFindSimilarBooksBasedOnCart"] = pageFindSimilarBooksBasedOnCart;
            }

            return View(await _bookService.GetByIdAsync(id));
        }
        public async Task<IActionResult> VoucherDetails(int id)
        {
            var voucher = await _voucherService.GetByIdAsync(id);
            return PartialView(voucher);
        }
        public async Task<IActionResult> HandlerFilter(BookFilterOptions filterOptions, int page = 1, int size = 10)
        {
            var result = await _bookService.GetFilteredBooks(filterOptions, page, size);
            return PartialView("/Views/PartialViews/_BooksPartial.cshtml", result.Items);
        }
        public async Task<IActionResult> GetTopSellingBooksByCategory(int categoryId = 0)
        {
            var result = await _bookService.GetTopSellingBooksByCategory(categoryId, 1, 5);
            return PartialView("/Views/PartialViews/_TopSellingBooksPartial.cshtml", result.Items);
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
        public async Task<IActionResult> GetCategories()
        {
            return PartialView(await _categoryService.GetAllAsync());
        }
        public async Task<IActionResult> Filter(BookFilterOptions filterOptions)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewData["categories"] = categories;

            var partnerTypes = await _partnerTypeService.GetAllAsync();
            ViewData["partnerTypes"] = partnerTypes;

            var authors = await _authorService.GetAllAsync();
            ViewData["authors"] = authors;

            var coverTypes = await _coverTypeService.GetAllAsync();
            ViewData["coverTypes"] = coverTypes;

            ViewData["Search"] = TempData["Search"];

            ViewData["filterOptions"] = filterOptions;

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterResult(BookFilterOptions filterOptions, int page = 1, int size = 20)
        {
            var result = await _bookService.GetFilteredBooks(filterOptions, page, size);
            return PartialView(result);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Search(string Search)
        {
            TempData["Search"] = Search;
            return RedirectToAction("Filter");
        }
    }
}

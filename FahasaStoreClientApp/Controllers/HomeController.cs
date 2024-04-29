using FahasaStoreClientApp.DataTemp.DataEntities;
using FahasaStoreClientApp.DataTemp.DataVM;
using FahasaStoreClientApp.Interfaces;
using FahasaStoreClientApp.Models;
using FahasaStoreClientApp.Models.Entities;
using FahasaStoreClientApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FahasaStoreClientApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Banners"] = new BannerData().ListBanners();

            ViewData["Menus"] = new MenuData().ListMenus();

            ViewData["FlashSale"] = new FlashSaleBookData().FlashSaleToday(20);

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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

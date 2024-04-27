using FahasaStoreClientApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreClientApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        public IActionResult List()
        {
            return View();
        }
        public async Task<IActionResult> Update()
        {
            ViewData["Provinces"] = await _addressService.GetListProvince();
            return View();
        }

        public async Task<IActionResult> Push()
        {
            ViewData["Provinces"] = await _addressService.GetListProvince();
            return View();
        }

        public async Task<IActionResult> OnGetDistricts(int provinceId)
        {
            var districts = await _addressService.GetListDistrictByProvinceId(provinceId);
            return PartialView("_DistrictsPartial", districts);
        }

        public async Task<IActionResult> OnGetWards(int districtId)
        {
            var wards = await _addressService.GetListWardByDistrictId(districtId);
            return PartialView("_WardsPartial", wards);
        }
    }
}

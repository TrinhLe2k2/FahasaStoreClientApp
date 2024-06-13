using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Filters;
using FahasaStoreClientApp.Helpers;
using FahasaStoreClientApp.Models;
using FahasaStoreClientApp.Models.EModels;
using FahasaStoreClientApp.Services;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FahasaStoreClientApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserLogined _userLogined;
        private readonly IReviewService _reviewService;
        private readonly ICartItemService _cartItemService;
        private readonly INotificationService _notificationService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IVoucherService _voucherService;
        private readonly IBookService _bookService;
        private readonly IImageUploader _imageUploader;

        public UserController(IUserService userService, UserLogined userLogined, IReviewService reviewService, ICartItemService cartItemService, INotificationService notificationService, IOrderService orderService, IImageUploader imageUploader, IAddressService addressService, IVoucherService voucherService, IBookService bookService)
        {
            _userService = userService;
            _userLogined = userLogined;
            _reviewService = reviewService;
            _cartItemService = cartItemService;
            _notificationService = notificationService;
            _orderService = orderService;
            _imageUploader = imageUploader;
            _addressService = addressService;
            _voucherService = voucherService;
            _bookService = bookService;
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> ControlPanel()
        {
            ViewData["CurrentUser"] = _userLogined.CurrentUser;
            var orders = await _orderService.GetListByAsync("UserId", _userLogined.CurrentUser?.Id ?? "");
            ViewData["Orders"] = orders.Items;
            return View();
        }
        [Authorize(roles: AppRole.Customer)]
        public IActionResult Pay()
        {
            return View();
        }
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Vouchers(int page = 1, int size = 5)
        {
            var pgaeVouchers = await _voucherService.GetPagination(page, size);
            return View(pgaeVouchers);
        }
        #region Information
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Information()
        {
            if (_userLogined.CurrentUser == null) return Json(new { success = false, message = "User not logged in" });
            var userEdit = await _userService.GetItemUpdateByIdAsync(_userLogined.CurrentUser.Id);
            return PartialView(userEdit);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[IgnoreAntiforgeryToken]
        public async Task<IActionResult> Information(AspNetUserModel model, IFormFile? fileImage)
        {
            if (_userLogined.CurrentUser == null) return Json(new { success = false, message = "User not logged in" });
            var userEdit = await _userService.GetItemUpdateByIdAsync(_userLogined.CurrentUser.Id);
            if (fileImage != null)
            {
                if (userEdit.PublicId != null && userEdit.ImageUrl != null)
                {
                    var isDeleteImg = await _imageUploader.RemoveImageAsync(userEdit.PublicId);
                }
                var resImgUploader = await _imageUploader.UploadImageAsync(fileImage, "User");
                userEdit.PublicId = resImgUploader.PublicId;
                userEdit.ImageUrl = resImgUploader.Url;

                _userLogined.CurrentUser.ImageUrl = resImgUploader.Url;
            }
            userEdit.FullName = model.FullName;
            userEdit.Email = model.Email;
            userEdit.PhoneNumber = model.PhoneNumber;

            var resUpdate = await _userService.UpdateAsync(_userLogined.CurrentUser.Id, userEdit);
            if (!string.IsNullOrEmpty(resUpdate))
            {
                var userUpdated = await _userService.GetByIdAsync(_userLogined.CurrentUser.Id);
                _userLogined.CurrentUser = userUpdated;
                return RedirectToAction("ControlPanel");
            }
            else
            {
                return Json(new { success = false, message = "Cập nhật thất bại" });
            }
        }
        #endregion
        #region Cart
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Cart()
        {
            if (_userLogined.CurrentUser == null || _userLogined.CurrentUser.Cart == null) return Json(new { success = false });
            var cartId = _userLogined.CurrentUser.Cart.Id;
            var pageCartItems = await _cartItemService.GetListByAsync("CartId", cartId.ToString(), 1, 100);
            return View(pageCartItems.Items);
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> GetCart()
        {
            if (_userLogined.CurrentUser == null || _userLogined.CurrentUser.Cart == null) return Json(new { success = false });
            var cartId = _userLogined.CurrentUser.Cart.Id;
            var pageCartItems = await _cartItemService.GetListByAsync("CartId", cartId.ToString(), 1, 5);
            return PartialView(pageCartItems);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddCartItem(CartItemModel model)
        {
            if(_userLogined.CurrentUser == null || _userLogined.CurrentUser.Cart == null) return Json(new { success = false });
            model.CartId = _userLogined.CurrentUser.Cart.Id;
            var res = await _cartItemService.AddAsync(model);
            return Json(new { CartItemModel = res });
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UpdateCartItem(int id, int quantity)
        {
            var CartItemEdit = await _cartItemService.GetItemUpdateByIdAsync(id);
            CartItemEdit.Quantity = quantity;
            CartItemEdit.CreatedAt = DateTime.Now;
            var res = await _cartItemService.UpdateAsync(id, CartItemEdit);
            return Json(new { CartItemModel = res });
        }
        [Authorize(roles: AppRole.Customer)]
        public async Task<int> GetIntoMoney(int[] cartItemIds)
        {
            var totalMoney = await _cartItemService.IntoMoney(cartItemIds);
            return totalMoney;
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var res = await _cartItemService.GetByIdAsync(id);
            return PartialView(res);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost, ActionName("DeleteCartItem")]
        [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ConfirmDeleteCartItem(int id)
        {
            var res = await _cartItemService.DeleteAsync(id);
            return Json(new { CartItemModel = res });
        }

        #endregion
        #region Order
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Orders(int page = 1, int size = 10)
        {
            if (_userLogined.CurrentUser == null) return Json(new { success = false });
            var pageOrders = await _orderService.GetListByAsync("UserId", _userLogined.CurrentUser.Id, page, size);
            return View(pageOrders);
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> OrderDetail(int id)
        {
            var od = await _orderService.GetByIdAsync(id);
            return PartialView(od);
        }
        #endregion
        #region Reviews
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Reviews(int page = 1, int size = 10)
        {
            if (_userLogined.CurrentUser == null) return Json(new { success = false, message = "User not logged in" });
            var pageReviews = await _reviewService.GetListByAsync("UserId", _userLogined.CurrentUser.Id, page, size);
            return View(pageReviews);
        }

        [Authorize(roles: AppRole.Customer)]
        public IActionResult AddReview(int id)
        {
            return PartialView();
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int id, ReviewModel model)
        {
            if (_userLogined.CurrentUser == null)
            {
                return Json(new { ReviewModel = "failed" });
            }
            model.Id = 0;
            model.BookId = id;
            model.UserId = _userLogined.CurrentUser.Id ;
            model.Active = true;

            var res = await _reviewService.AddAsync(model);
            return RedirectToAction("Reviews");
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> EditReview(int id)
        {
            var review = await _reviewService.GetItemUpdateByIdAsync(id);
            return PartialView(review);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(int id, ReviewModel model)
        {
            var reviewEdit = await _reviewService.GetItemUpdateByIdAsync(id);

            model.Id = reviewEdit.Id;
            model.BookId = reviewEdit.BookId;
            model.UserId = reviewEdit.UserId;
            model.Active = reviewEdit.Active;
            model.CreatedAt = reviewEdit.CreatedAt;

            var res = await _reviewService.UpdateAsync(id, model);
            return RedirectToAction("Reviews");
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetItemUpdateByIdAsync(id);
            return PartialView(review);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            var isdeleted = await _reviewService.DeleteAsync(id);
            if (isdeleted)
            {
                TempData["SuccessMessage"] = "Xóa thành công.";
                return Json(new {success = true});
            }
            TempData["ErrorMessage"] = "Không thể xóa dữ liệu.";
            return RedirectToAction("Reviews");
        }
        #endregion
        #region Notifications
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Notifications(int page = 1, int size = 10)
        {
            if (_userLogined.CurrentUser == null) return Json(new { success = false });
            return View(await _notificationService.GetListByAsync("UserId", _userLogined.CurrentUser.Id, page, size));
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> NotificationDetails(int id)
        {
           var noti = await _notificationService.GetByIdAsync(id);
            return PartialView(noti);
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> GetNotifications()
        {
            if (_userLogined.CurrentUser == null) return Json(new {success = false});
            return PartialView(await _notificationService.GetListByAsync("UserId", _userLogined.CurrentUser.Id, 1, 5));
        }
        #endregion
        #region Address
        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> Address(int page = 1, int size = 10)
        {
            if(_userLogined.CurrentUser == null) return Json(new {Success = false});
            var pageAddress = await _addressService.GetListByAsync("UserId", _userLogined.CurrentUser.Id, page, size);
            return View(pageAddress);
        }

        [Authorize(roles: AppRole.Customer)]
        public IActionResult AddressPush()
        {
            return PartialView();
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressPush(AddressModel model)
        {
            try
            {
                if (_userLogined.CurrentUser == null)
                {
                    return Json(new { AddressModel = "failed" });
                }
                model.UserId = _userLogined.CurrentUser.Id;

                var res = await _addressService.AddAsync(model);
                TempData["SuccessMessage"] = "Thêm thành công.";
                return RedirectToAction("Address");
            }
            catch
            {
                TempData["ErrorMessage"] = "Thêm thất bại.";
                return RedirectToAction("Address");
            }
        }

        [Authorize(roles: AppRole.Customer)]
        public async Task<IActionResult> AddressUpdate(int id)
        {
            var addressUpdate = await _addressService.GetItemUpdateByIdAsync(id);
            return PartialView(addressUpdate);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressUpdate(int id, AddressModel model)
        {
            try
            {
                var addressUpdate = await _addressService.GetItemUpdateByIdAsync(id);
                //model.Id = addressUpdate.Id;
                model.UserId = addressUpdate.UserId;
                model.CreatedAt = addressUpdate.CreatedAt;
                var res = await _addressService.UpdateAsync(id, model);
                TempData["SuccessMessage"] = "Cập nhật thành công.";
                return RedirectToAction("Address");
            }
            catch
            {
                TempData["ErrorMessage"] = "Cập nhật thất bại.";
                return RedirectToAction("Address");
            }
        }

        [Authorize(roles: AppRole.Customer)] 
        public async Task<IActionResult> AddressDelete(int id)
        {
            var addressDel = await _addressService.GetItemUpdateByIdAsync(id);
            return PartialView(addressDel);
        }

        [Authorize(roles: AppRole.Customer)]
        [HttpPost, ActionName("AddressDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressDeleteConfirmed(int id)
        {
            var isdeleted = await _addressService.DeleteAsync(id);
            if (isdeleted)
            {
                TempData["SuccessMessage"] = "Xóa thành công.";
                return RedirectToAction("Address");
            }
            TempData["ErrorMessage"] = "Xóa thất bại.";
            return RedirectToAction("Address");
        }

        public async Task<IActionResult> OnGetProvinces(string? province)
        {
            var provinces = await _addressService.GetListProvince();
            ViewData["Province"] = province;
            return PartialView(provinces);
        }

        public async Task<IActionResult> OnGetDistricts(string? district, int provinceId)
        {
            var districts = await _addressService.GetListDistrictByProvinceId(provinceId);
            ViewData["District"] = district;
            return PartialView(districts);
        }

        public async Task<IActionResult> OnGetWards(string? ward, int districtId)
        {
            var wards = await _addressService.GetListWardByDistrictId(districtId);
            ViewData["Ward"] = ward;
            return PartialView(wards);
        }
        #endregion
    }
}

using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Models;
using FahasaStoreClientApp.Models.CustomModels;
using FahasaStoreClientApp.Models.DTO;
using FahasaStoreClientApp.Models.EModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FahasaStoreClientApp.Services.EntityService
{
    public interface IAddressService : IGenericService<Address, AddressModel, AddressDTO, int> 
    {
        Task<ICollection<Province>> GetListProvince();
        Task<ICollection<District>> GetListDistrictByProvinceId(int provinceId);
        Task<ICollection<Ward>> GetListWardByDistrictId(int districtId);
    }
    public class AddressService : GenericService<Address, AddressModel, AddressDTO, int>, IAddressService
    {
        public AddressService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Address") { }
        public async Task<ICollection<Province>> GetListProvince()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listProvince");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<Province>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }
        public async Task<ICollection<District>> GetListDistrictByProvinceId(int provinceId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listDistrict?provinceId=" + provinceId);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        var provinces = JsonConvert.DeserializeObject<List<District>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<District>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }
        public async Task<ICollection<Ward>> GetListWardByDistrictId(int districtId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listWards?districtId=" + districtId);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        var provinces = JsonConvert.DeserializeObject<List<Ward>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<Ward>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }
    }

    public interface IAuthorService : IGenericService<Author, AuthorModel, AuthorDTO, int> { }
    public class AuthorService : GenericService<Author, AuthorModel, AuthorDTO, int>, IAuthorService
    {
        public AuthorService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Authors") { }
    }

    public interface IBannerService : IGenericService<Banner, BannerModel, BannerDTO, int> { }
    public class BannerService : GenericService<Banner, BannerModel, BannerDTO, int>, IBannerService
    {
        public BannerService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Banners") { }
    }

    public interface IBookService : IGenericService<Book, BookModel, BookDTO, int> 
    {
        Task<PaginatedResponse<BookDTO>> GetMonthlyTrendingBooks(int page = 1, int size = 10);
        Task<PaginatedResponse<BookDTO>> GetYearlyTrendingBooks(int page = 1, int size = 10);
        Task<PaginatedResponse<BookDTO>> GetTopSellingBooksByCategory(int categoryId, int page = 1, int size = 10);
        Task<PaginatedResponse<BookDTO>> GetFilteredBooks(BookFilterOptions filterOptions, int page = 1, int size = 10);
        Task<ICollection<BookDTO>> SimilarBooks(int id, int size = 10);
        Task<bool> HasUserPurchasedBook(string userId, int bookId);
        Task<PaginatedResponse<BookDTO>> FindSimilarBooksBasedOnCart(int cartId, int page = 1, int size = 10, string aggregationMethod = "average");
    }
    public class BookService : GenericService<Book, BookModel, BookDTO, int>, IBookService
    {
        public BookService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Books") { }

        public async Task<PaginatedResponse<BookDTO>> GetMonthlyTrendingBooks(int page = 1, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/trending/monthly?page={page}&size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
                    return data ?? new PaginatedResponse<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
        public async Task<PaginatedResponse<BookDTO>> GetYearlyTrendingBooks(int page = 1, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/trending/yearly?page={page}&size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
                    return data ?? new PaginatedResponse<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
        public async Task<PaginatedResponse<BookDTO>> GetTopSellingBooksByCategory(int categoryId, int page = 1, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/TopSellingByCategory/{categoryId}?page={page}&size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
                    return data ?? new PaginatedResponse<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
        public async Task<PaginatedResponse<BookDTO>> GetFilteredBooks(BookFilterOptions filterOptions, int page = 1, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Chuyển đổi filterOptions thành query string
                    var query = new List<string>();
                    if (filterOptions != null)
                    {
                        if (filterOptions.CategoryId != null) query.Add($"CategoryId={filterOptions.CategoryId}");
                        if (filterOptions.SubcategoryId != null) query.Add($"SubcategoryId={filterOptions.SubcategoryId}");
                        if (filterOptions.AuthorId != null) query.Add($"AuthorId={filterOptions.AuthorId}");
                        if (filterOptions.MinPrice != null) query.Add($"MinPrice={filterOptions.MinPrice}");
                        if (filterOptions.MaxPrice != null) query.Add($"MaxPrice={filterOptions.MaxPrice}");
                        if (filterOptions.PartnerId != null) query.Add($"PartnerId={filterOptions.PartnerId}");
                        if (filterOptions.CoverTypeId != null) query.Add($"CoverTypeId={filterOptions.CoverTypeId}");
                        if (!filterOptions.SortBy.IsNullOrEmpty()) query.Add($"SortBy={filterOptions.SortBy}");
                        if (!filterOptions.Search.IsNullOrEmpty()) query.Add($"Search={filterOptions.Search}");

                        // Thêm các điều kiện khác của filterOptions ở đây
                    }
                    query.Add($"page={page}");
                    query.Add($"size={size}");
                    var queryString = string.Join("&", query);
                    var response = await httpClient.GetAsync($"{_apiUrl}/Filter?{queryString}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
                    return data ?? new PaginatedResponse<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
        
        //public async Task<PaginatedResponse<BookDTO>> GetFilteredBooks(BookFilterOptions filterOptions, int page = 1, int size = 10)
        //{
        //    try
        //    {
        //        using (var httpClient = _httpClientFactory.CreateClient())
        //        {
        //            // Tạo đối tượng gửi trong phần thân của yêu cầu POST
        //            var postData = new
        //            {
        //                filterOptions = filterOptions,
        //                page = page,
        //                size = size
        //            };

        //            // Chuyển đổi đối tượng thành JSON
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

        //            // Gửi yêu cầu POST
        //            var response = await httpClient.PostAsync($"{_apiUrl}/Filter", jsonContent);
        //            response.EnsureSuccessStatusCode();

        //            // Đọc nội dung phản hồi
        //            var content = await response.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
        //            return data ?? new PaginatedResponse<BookDTO>();
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new Exception("Error occurred while fetching list from API.", ex);
        //    }
        //    catch (JsonException ex)
        //    {
        //        throw new Exception("Error occurred while parsing JSON response.", ex);
        //    }
        //}


        public async Task<ICollection<BookDTO>> SimilarBooks(int id, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/SimilarBooks/{id}?size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ICollection<BookDTO>>(content);
                    return data ?? new List<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
        public async Task<bool> HasUserPurchasedBook(string userId, int bookId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/HasUserPurchasedBook?userId={userId}&bookId={bookId}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<bool>(content);
                    return data;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

        public async Task<PaginatedResponse<BookDTO>> FindSimilarBooksBasedOnCart(int cartId, int page = 1, int size = 10, string aggregationMethod = "average")
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/FindSimilarBooksBasedOnCart/{cartId}?page={page}&size={size}&aggregationMethod={aggregationMethod}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<BookDTO>>(content);
                    return data ?? new PaginatedResponse<BookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

    }

    public interface IBookPartnerService : IGenericService<BookPartner, BookPartnerModel, BookPartnerDTO, int> { }
    public class BookPartnerService : GenericService<BookPartner, BookPartnerModel, BookPartnerDTO, int>, IBookPartnerService
    {
        public BookPartnerService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/BookPartners") { }
    }

    public interface ICartService : IGenericService<Cart, CartModel, CartDTO, int> 
    {
        
    }
    public class CartService : GenericService<Cart, CartModel, CartDTO, int>, ICartService
    {
        public CartService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Carts") { }
    }

    public interface ICartItemService : IGenericService<CartItem, CartItemModel, CartItemDTO, int> 
    {
        Task<int> IntoMoney(int[] cartItemIds);
    }
    public class CartItemService : GenericService<CartItem, CartItemModel, CartItemDTO, int>, ICartItemService
    {
        public CartItemService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/CartItems") { }
        public async Task<int> IntoMoney(int[] cartItemIds)
        {
            if (cartItemIds == null || cartItemIds.Length == 0)
            {
                throw new ArgumentException("Cart item IDs are required.", nameof(cartItemIds));
            }

            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Construct the query string
                    var queryString = string.Join("&", cartItemIds.Select(id => $"cartItemIds={id}"));
                    var requestUrl = $"{_apiUrl}/IntoMoney?{queryString}";

                    var response = await httpClient.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<int>(content);
                    return data;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error occurred while fetching total money from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
    }

    public interface ICategoryService : IGenericService<Category, CategoryModel, CategoryDTO, int> { }
    public class CategoryService : GenericService<Category, CategoryModel, CategoryDTO, int>, ICategoryService
    {
        public CategoryService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Categories") { }
    }


    public interface ICoverTypeService : IGenericService<CoverType, CoverTypeModel, CoverTypeDTO, int> { }
    public class CoverTypeService : GenericService<CoverType, CoverTypeModel, CoverTypeDTO, int>, ICoverTypeService
    {
        public CoverTypeService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/CoverTypes") { }
    }

    public interface IDimensionService : IGenericService<Dimension, DimensionModel, DimensionDTO, int> { }
    public class DimensionService : GenericService<Dimension, DimensionModel, DimensionDTO, int>, IDimensionService
    {
        public DimensionService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Dimensions") { }
    }

    public interface IFavouriteService : IGenericService<Favourite, FavouriteModel, FavouriteDTO, int> { }
    public class FavouriteService : GenericService<Favourite, FavouriteModel, FavouriteDTO, int>, IFavouriteService
    {
        public FavouriteService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Favourites") { }
    }

    public interface IFlashSaleService : IGenericService<FlashSale, FlashSaleModel, FlashSaleDTO, int> { }
    public class FlashSaleService : GenericService<FlashSale, FlashSaleModel, FlashSaleDTO, int>, IFlashSaleService
    {
        public FlashSaleService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/FlashSales") { }
    }

    public interface IFlashSaleBookService : IGenericService<FlashSaleBook, FlashSaleBookModel, FlashSaleBookDTO, int> 
    {
        Task<PaginatedResponse<FlashSaleBookDTO>> GetFlashSaleBooksToday(int page = 1, int size = 10);
    }
    public class FlashSaleBookService : GenericService<FlashSaleBook, FlashSaleBookModel, FlashSaleBookDTO, int>, IFlashSaleBookService
    {
        public FlashSaleBookService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/FlashSaleBooks") {}
        public async Task<PaginatedResponse<FlashSaleBookDTO>> GetFlashSaleBooksToday(int page = 1, int size = 10)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/GetFlashSaleBooksToday?page={page}&size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<FlashSaleBookDTO>>(content);
                    return data ?? new PaginatedResponse<FlashSaleBookDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
    }

    public interface IMenuService : IGenericService<Menu, MenuModel, MenuDTO, int> { }
    public class MenuService : GenericService<Menu, MenuModel, MenuDTO, int>, IMenuService
    {
        public MenuService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Menus") { }
    }

    public interface INotificationService : IGenericService<Notification, NotificationModel, NotificationDTO, int> { }
    public class NotificationService : GenericService<Notification, NotificationModel, NotificationDTO, int>, INotificationService
    {
        public NotificationService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Notifications") { }
    }

    public interface INotificationTypeService : IGenericService<NotificationType, NotificationTypeModel, NotificationTypeDTO, int> { }
    public class NotificationTypeService : GenericService<NotificationType, NotificationTypeModel, NotificationTypeDTO, int>, INotificationTypeService
    {
        public NotificationTypeService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/NotificationTypes") { }
    }

    public interface IOrderService : IGenericService<Order, OrderModel, OrderDTO, int> { }
    public class OrderService : GenericService<Order, OrderModel, OrderDTO, int>, IOrderService
    {
        public OrderService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Orders") { }
    }

    public interface IOrderItemService : IGenericService<OrderItem, OrderItemModel, OrderItemDTO, int> { }
    public class OrderItemService : GenericService<OrderItem, OrderItemModel, OrderItemDTO, int>, IOrderItemService
    {
        public OrderItemService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/OrderItems") { }
    }

    public interface IOrderStatusService : IGenericService<OrderStatus, OrderStatusModel, OrderStatusDTO, int> { }
    public class OrderStatusService : GenericService<OrderStatus, OrderStatusModel, OrderStatusDTO, int>, IOrderStatusService
    {
        public OrderStatusService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/OrderStatus") { }
    }

    public interface IPartnerTypeService : IGenericService<PartnerType, PartnerTypeModel, PartnerTypeDTO, int> { }
    public class PartnerTypeService : GenericService<PartnerType, PartnerTypeModel, PartnerTypeDTO, int>, IPartnerTypeService
    {
        public PartnerTypeService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/PartnerTypes") { }
    }

    public interface IPaymentService : IGenericService<Payment, PaymentModel, PaymentDTO, int> { }
    public class PaymentService : GenericService<Payment, PaymentModel, PaymentDTO, int>, IPaymentService
    {
        public PaymentService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Payments") { }
    }

    public interface IPaymentMethodService : IGenericService<PaymentMethod, PaymentMethodModel, PaymentMethodDTO, int> { }
    public class PaymentMethodService : GenericService<PaymentMethod, PaymentMethodModel, PaymentMethodDTO, int>, IPaymentMethodService
    {
        public PaymentMethodService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/PaymentMethods") { }
    }

    public interface IPlatformService : IGenericService<Platform, PlatformModel, PlatformDTO, int> { }
    public class PlatformService : GenericService<Platform, PlatformModel, PlatformDTO, int>, IPlatformService
    {
        public PlatformService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Platforms") { }
    }

    public interface IPosterImageService : IGenericService<PosterImage, PosterImageModel, PosterImageDTO, int> { }
    public class PosterImageService : GenericService<PosterImage, PosterImageModel, PosterImageDTO, int>, IPosterImageService
    {
        public PosterImageService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/PosterImages") { }
    }

    public interface IReviewService : IGenericService<Review, ReviewModel, ReviewDTO, int> { }
    public class ReviewService : GenericService<Review, ReviewModel, ReviewDTO, int>, IReviewService
    {
        public ReviewService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Reviews") { }
    }

    public interface IStatusService : IGenericService<Status, StatusModel, StatusDTO, int> { }
    public class StatusService : GenericService<Status, StatusModel, StatusDTO, int>, IStatusService
    {
        public StatusService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Status") { }
    }

    public interface ITopicService : IGenericService<Topic, TopicModel, TopicDTO, int> { }
    public class TopicService : GenericService<Topic, TopicModel, TopicDTO, int>, ITopicService
    {
        public TopicService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Topics") { }
    }

    public interface ITopicContentService : IGenericService<TopicContent, TopicContentModel, TopicContentDTO, int> { }
    public class TopicContentService : GenericService<TopicContent, TopicContentModel, TopicContentDTO, int>, ITopicContentService
    {
        public TopicContentService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/TopicContents") { }
    }

    public interface IVoucherService : IGenericService<Voucher, VoucherModel, VoucherDTO, int> 
    {
        Task<PaginatedResponse<VoucherDTO>> GetVouchersToday(int page = 1, int size = 50);
    }
    public class VoucherService : GenericService<Voucher, VoucherModel, VoucherDTO, int>, IVoucherService
    {
        public VoucherService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Vouchers") { }
        public async Task<PaginatedResponse<VoucherDTO>> GetVouchersToday(int page = 1, int size = 50)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/today?page={page}&size={size}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PaginatedResponse<VoucherDTO>>(content);
                    return data ?? new PaginatedResponse<VoucherDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching list from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }
    }

    public interface IWebsiteService : IGenericService<Website, WebsiteModel, WebsiteDTO, int> { }
    public class WebsiteService : GenericService<Website, WebsiteModel, WebsiteDTO, int>, IWebsiteService
    {
        public WebsiteService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Websites") { }
    }

    public interface IUserService : IGenericService<AspNetUser, AspNetUserModel, AspNetUserDTO, string> { }
    public class UserService : GenericService<AspNetUser, AspNetUserModel, AspNetUserDTO, string>, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Users") { }
    }

    public interface IRoleService : IGenericService<AspNetRole, AspNetRoleModel, AspNetRoleDTO, string> { }
    public class RoleService : GenericService<AspNetRole, AspNetRoleModel, AspNetRoleDTO, string>, IRoleService
    {
        public RoleService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Roles") { }
    }
    public interface IPartnerService : IGenericService<Partner, PartnerModel, PartnerDTO, int>
    {
    }
    public class PartnerService : GenericService<Partner, PartnerModel, PartnerDTO, int>, IPartnerService
    {
        public PartnerService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Partners") { }

    }
    public interface ISubcategoryService : IGenericService<Subcategory, SubcategoryModel, SubcategoryDTO, int>
    {
    }
    public class SubcategoryService : GenericService<Subcategory, SubcategoryModel, SubcategoryDTO, int>, ISubcategoryService
    {
        public SubcategoryService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Subcategories") { }

    }
}

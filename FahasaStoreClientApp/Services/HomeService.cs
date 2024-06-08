using FahasaStoreClientApp.Entities;
using Newtonsoft.Json;

namespace FahasaStoreClientApp.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync();
        Task<IEnumerable<Banner>> GetBannersAsync();
        Task<IEnumerable<Menu>> GetMenusAsync();
        Task<FlashSale> GetFlashSaleTodayAsync();
        Task<IEnumerable<Book>> GetShoppingTrendByDayMonthYearAsync();
        Task<IEnumerable<Banner>> GetPartnersAsync();
    }
    public class HomeService : IHomeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Banner>> GetBannersAsync()
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7069/api/Banners");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<Banner>>(content);
                    return data ?? new List<Banner>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý exception, có thể ghi log hoặc thực hiện các hành động phù hợp khác
                throw new Exception("Error occurred while fetching Banners from API.", ex);
            }
            catch (JsonException ex)
            {
                // Xử lý exception phân tích cú pháp JSON
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7069/api/Categories");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
                    return data ?? new List<Category>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi HomeService: GetCategories", ex);
            }
        }

        public Task<Category> GetCategoryByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FlashSale> GetFlashSaleTodayAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7069/api/Menus");

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException("Unauthorized access - please log in.");
                    }

                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<Menu>>(content);
                    return data ?? new List<Menu>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý exception, có thể ghi log hoặc thực hiện các hành động phù hợp khác
                throw new Exception("Error occurred while fetching Menus from API.", ex);
            }
            catch (JsonException ex)
            {
                // Xử lý exception phân tích cú pháp JSON
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

        public Task<IEnumerable<Banner>> GetPartnersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetShoppingTrendByDayMonthYearAsync()
        {
            throw new NotImplementedException();
        }
    }
}

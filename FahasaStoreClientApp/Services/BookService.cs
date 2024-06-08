using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Models.EModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FahasaStoreClientApp.Services
{
    public interface IBookService : IGenericService<Book, BookModel, int>
    {
        Task<ICollection<Book>> GetDailyTrendingBooks();
        Task<ICollection<Book>> GetMonthlyTrendingBooks();
        Task<ICollection<Book>> GetYearlyTrendingBooks();
    }
    public class BookService : GenericService<Book, BookModel, int>, IBookService
    {
        public BookService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, "https://localhost:7069/api/Books") { }

        private async Task<ICollection<Book>> GetTrendingBooks(string date)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_apiUrl}/trending/{date}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ICollection<Book>>(content);
                    return data ?? new List<Book>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error occurred while fetching data from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

        public async Task<ICollection<Book>> GetDailyTrendingBooks()
        {
            return await GetTrendingBooks("daily");
        }

        public async Task<ICollection<Book>> GetMonthlyTrendingBooks()
        {
            return await GetTrendingBooks("monthly");
        }

        public async Task<ICollection<Book>> GetYearlyTrendingBooks()
        {
            return await GetTrendingBooks("yearly");
        }
    }
}

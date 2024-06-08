using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FahasaStoreClientApp.Services
{
    public interface IUserService
    {
        Task<AspNetUser> GetProfile();
        Task<Cart> GetCart();
        Task<ICollection<Address>> GetAddresses();
        Task<ICollection<Favourite>> GetFavourites();
        Task<ICollection<Notification>> GetNotifications();
        Task<ICollection<Order>> GetOrders();
        Task<ICollection<Review>> GetReviews();
    }

    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserLogined _userLogined;

        public UserService(IHttpClientFactory httpClientFactory, UserLogined userLogined)
        {
            _httpClientFactory = httpClientFactory;
            _userLogined = userLogined;
        }

        private async Task<AspNetUser> FetchUserFromApi()
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"https://localhost:7069/api/Users/{_userLogined.UserId}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<AspNetUser>(content);
                    return data ?? new AspNetUser();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error occurred while fetching User with ID {_userLogined.UserId} from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while parsing JSON response.", ex);
            }
        }

        public async Task<ICollection<Address>> GetAddresses()
        {
            var profile = await FetchUserFromApi();
            return profile?.Addresses ?? new List<Address>();
        }

        public async Task<Cart> GetCart()
        {
            var profile = await FetchUserFromApi();
            return profile?.Cart ?? new Cart();
        }

        public async Task<ICollection<Favourite>> GetFavourites()
        {
            var profile = await FetchUserFromApi();
            return profile?.Favourites ?? new List<Favourite>();
        }

        public async Task<AspNetUser> GetProfile()
        {
            return await FetchUserFromApi();
        }

        public async Task<ICollection<Notification>> GetNotifications()
        {
            var profile = await FetchUserFromApi();
            return profile?.Notifications ?? new List<Notification>();
        }

        public async Task<ICollection<Order>> GetOrders()
        {
            var profile = await FetchUserFromApi();
            return profile?.Orders ?? new List<Order>();
        }

        public async Task<ICollection<Review>> GetReviews()
        {
            var profile = await FetchUserFromApi();
            return profile?.Reviews ?? new List<Review>();
        }
    }
}

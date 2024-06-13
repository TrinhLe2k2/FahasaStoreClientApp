using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Models;
using FahasaStoreClientApp.Models.DTO;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FahasaStoreClientApp.Helpers
{
    public class UserLogined
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLogined(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AspNetUserDTO? CurrentUser
        {
            get
            {
                var userJson = _httpContextAccessor.HttpContext?.Session.GetString("User");
                if (userJson == null) return null;
                return JsonConvert.DeserializeObject<AspNetUserDTO>(userJson) ?? null;
            }
            set
            {
                var userJson = JsonConvert.SerializeObject(value);
                _httpContextAccessor.HttpContext?.Session.SetString("User", userJson);
            }
        }

        public string? JWToken
        {
            get => _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            set
            {
                if (value == null)
                {
                    _httpContextAccessor.HttpContext?.Session.Remove("JWToken");
                }
                else
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("JWToken", value);
                }
            }
        }
    }
}

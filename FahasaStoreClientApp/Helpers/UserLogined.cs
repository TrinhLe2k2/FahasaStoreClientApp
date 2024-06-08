using FahasaStoreClientApp.Entities;
using FahasaStoreClientApp.Services.EntityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FahasaStoreClientApp.Helpers
{
    public class UserLogined
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLogined(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.Session.GetString("UserId") ?? "986de402-9a91-406f-8269-8705beb3a505";
        public string FullName => _httpContextAccessor.HttpContext?.Session.GetString("FullName") ?? "Trình Lê";
        public string Avatar => _httpContextAccessor.HttpContext?.Session.GetString("Avatar") ?? string.Empty;
        public string AccessToken => _httpContextAccessor.HttpContext?.Session.GetString("JWToken") ?? "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI5ODZkZTQwMi05YTkxLTQwNmYtODI2OS04NzA1YmViM2E1MDUiLCJBdmF0YXIiOiJodHRwczovL3Jlcy1jb25zb2xlLmNsb3VkaW5hcnkuY29tL2RyazgzenFncy9tZWRpYV9leHBsb3Jlcl90aHVtYm5haWxzLzZkNmE1YjBlOGM1ZjE5NTRmMjliNjA5MjAyODIxNzQ1L2RldGFpbGVkIiwiRnVsbE5hbWUiOiJUcsOsbmggTMOqIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidXNlckBnbWFpbC5jb20iLCJqdGkiOiIyOWU4MTlhMy1hYzMxLTRjY2YtYTgwMy03NTBmOGVjMmU2YTYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImV4cCI6MTczMzU3OTk0NywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA2OSIsImF1ZCI6IlVzZXIifQ.r--yP1UBc3P66ev4pAoCRM9B2IDeS0pefjPGP1U3V75_Wu7OHxVfdH3q9huX_kLRyz3x4xhSKCuAqANWrJ8ssA";
        
    }
}

using AutoMapper;
using FahasaStoreClientApp.Helpers;
using FahasaStoreClientApp.Interfaces;
using FahasaStoreClientApp.Services;
using Microsoft.AspNetCore.Localization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtTokenDecoder, JwtTokenDecoder>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IImageUploader, ImageUploader>();

builder.Services.AddScopedServicesFromAssembly(Assembly.GetExecutingAssembly(), "FahasaStoreClientApp.Services.EntityService");

builder.Services.AddScoped<UserLogined>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new UserLogined(httpContextAccessor);
});

// Thêm hỗ trợ session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession(); // Kích hoạt middleware session

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

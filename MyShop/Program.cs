using Domain;
using Domain.Repository.Interfaces;
using Domain.Repository;
using BLL.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MCSteklo.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession();

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyShopDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyShopConnectionString")));

            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICompareSevice, CompareService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IPAService, PAService>();
            //builder.Services.AddScoped<IPAVService, PAVService>();

            builder.Services.AddScoped<IPrPAV, PrPAV>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICompareRepository, CompareRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IPARepository, PARepository>();
            builder.Services.AddScoped<IPAVRepository, PAVRepository>();
            builder.Services.AddScoped<IUOW, UOW>();


            builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/account/login");
            builder.Services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<MyShopDb>()
            .AddDefaultTokenProviders();
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(opt =>
       {
           opt.LoginPath = new PathString("/account/login"); // create action with name Login
           opt.Cookie.HttpOnly = true;
           opt.Cookie.Name = "SessionId";
       });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapControllerRoute(
           name: "Admin",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<CookieManage>();
            app.Run();
        }
    }
}
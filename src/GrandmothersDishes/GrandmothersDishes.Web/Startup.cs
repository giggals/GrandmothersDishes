using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GrandmothersDishes.Data;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.Users.Contracts;
using GrandmothersDishes.Web.Middlewares.MiddlewareExtensions;
using GrandmothersDishes.Data.RepositoryPattern;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Web.ViewModels.Account;
using AutoMapper;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.EmployeeService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.HomeService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;


namespace GrandmothersDishes.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<GrandmothersDishesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<GrandMothersUser , IdentityRole>(opts =>
                {
                    opts.SignIn.RequireConfirmedEmail = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireDigit = false;
                    opts.Password.RequiredUniqueChars = 0;
                    opts.Password.RequiredLength = 3;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<GrandmothersDishesDbContext>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(conf =>
            {
                conf.CreateMap<RegisterViewModel, GrandMothersUser>();
                conf.CreateMap<CreateDishViewModel, Dish>();
                conf.CreateMap<Dish, DetailsDishViewModel>();
                conf.CreateMap<Dish, UpdateDeleteViewModel>();
                conf.CreateMap<CreateVehicleViewModel, Vehicle>();
                conf.CreateMap<CreateEmployeeViewModel, Employee>();
                conf.CreateMap<CreateDrinkViewModel, Drink>();
                conf.CreateMap<Drink, DrinkDetailsViewModel>();
            });

            services.AddLogging();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IFoodService , FoodService>();
            services.AddScoped<IVehicleService , VehicleService>();
            services.AddScoped<IEmployeeService, EmployeeService>(); 
            services.AddScoped<IDrinkService, DrinksService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
        }

        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            IServiceProvider provider,
            GrandmothersDishesDbContext dbContext
          )
        {
            AutoMapperConfig.RegisterMappings(
                        typeof(RegisterViewModel).Assembly
                );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSeedRolesMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

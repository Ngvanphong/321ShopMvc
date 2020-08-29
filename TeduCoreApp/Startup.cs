using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using System;
using TeduCoreApp.Application.Implementation;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Helpers;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Services;

namespace TeduCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("TeduCoreApp.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Indentity
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            //Config Indentity
            services.Configure<IdentityOptions>(option =>
            {
                //password setting
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                //lock setting
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1440);
                option.Lockout.MaxFailedAccessAttempts = 10;
                // check had email
                option.User.RequireUniqueEmail = true;
            });

            //section setting
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(180);
                options.Cookie.HttpOnly = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            // Seek database
            services.AddTransient<DbInitializer>();

            //Automapper
            services.AddAutoMapper();
            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            //UnitOfWork
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            //Cliam
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomerClaimsPrincipalFactory>();

            //Repository
            services.AddTransient<IRepository<ProductCategory, int>, EFRepository<ProductCategory, int>>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IRepository<Product, int>, EFRepository<Product, int>>();
            services.AddTransient<IRepository<Slide, int>, EFRepository<Slide, int>>();
            services.AddTransient<IRepository<Data.Entities.Contact, string>, EFRepository<Data.Entities.Contact, string>>();
            services.AddTransient<IRepository<SystemConfig, string>, EFRepository<SystemConfig, string>>();

            // Service
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ISystemConfigService, SystemConfigService>();

            services.AddMvc().AddJsonOptions(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            logerFactory.AddFile("Logs/tedu-{Date}.txt");

            app.UseMvc(routes =>
            {
            routes.MapRoute(

               name: "home",
               template: "index.html",
               defaults: new { controller = "Home", action = "Index"}
                   );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
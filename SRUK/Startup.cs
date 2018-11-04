using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SRUK.Data;
using SRUK.Models;
using SRUK.Entities;
using SRUK.Services;
using SRUK.Services.Interfaces;
using SRUK.Data.Entities;
using SRUK.Models.ManageViewModels;

namespace SRUK
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var lockoutOptions = new LockoutOptions()
            {
                AllowedForNewUsers = true,
                DefaultLockoutTimeSpan = TimeSpan.FromHours(1),
                MaxFailedAccessAttempts = 10
            };

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>{options.Lockout = lockoutOptions;})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //Users
                cfg.CreateMap<ApplicationUser, UserDTO>();
                cfg.CreateMap<ApplicationUser, UserIndexViewModel>();
                cfg.CreateMap<ApplicationUser, UserDetailsViewModel>();
                cfg.CreateMap<ApplicationUser, UserEditViewModel>();
                cfg.CreateMap<ApplicationUser, UserShortDTO>();
                cfg.CreateMap<ApplicationUser, IndexViewModel>();


                cfg.CreateMap<IndexViewModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opt=>opt.MapFrom(src=> src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
                cfg.CreateMap<UserCreateViewModel, ApplicationUser>();
                cfg.CreateMap<UserEditViewModel, ApplicationUser>();

                //Roles
                cfg.CreateMap<ApplicationRole, RoleDTO>();

                //Seasons
                cfg.CreateMap<Season, SeasonDTO>();
                cfg.CreateMap<Season, SeasonShortDTO>();
                cfg.CreateMap<SeasonDTO, Season>();
                cfg.CreateMap<SeasonCreateViewModel, SeasonDTO>();
                cfg.CreateMap<SeasonDTO, SeasonEditViewModel>();
                cfg.CreateMap<SeasonEditViewModel, SeasonDTO>();
                cfg.CreateMap<SeasonDTO, SeasonDeleteViewModel>();

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

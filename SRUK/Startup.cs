﻿using System;
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
using System.Globalization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using EntityFrameworkPaginate;

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
                DefaultLockoutTimeSpan = TimeSpan.FromHours(2),
                MaxFailedAccessAttempts = 10
            };

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>{options.Lockout = lockoutOptions;})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IPaperRepository, PaperRepository>();
            services.AddScoped<IPaperVersionRepository, PaperVersionRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IParticipanciesRepository, ParticipanciesRepository>();

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
                cfg.CreateMap<ApplicationUser, UserEditViewModel>();
                cfg.CreateMap<ApplicationUser, UserShortDTO>();
                cfg.CreateMap<ApplicationUser, IndexViewModel>();
                cfg.CreateMap<Page<ApplicationUser>,Page<UserShortDTO>>();
                cfg.CreateMap<UserDTO, UserDetailsViewModel>();


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
                cfg.CreateMap<SeasonDTO, SeasonDetailsViewModel>();

                //Participations
                cfg.CreateMap<Participancy, ParticipancyDTO>();
                cfg.CreateMap<ParticipancyDTO, Participancy>();
                cfg.CreateMap<Participancy, ParticipancyShortDTO>(); 
                cfg.CreateMap<ParticipancySignUpViewModel, ParticipancyDTO>();
                cfg.CreateMap<ParticipancyDTO, ParticipancyEditViewModel>();
                cfg.CreateMap<ParticipancyEditViewModel, ParticipancyDTO>();

                //Papers
                cfg.CreateMap<Paper, PaperDTO>(); 
                cfg.CreateMap<Paper, PaperShortDTO>()
                .ForMember(dest => dest.VersionsNo, opt => opt.MapFrom(src => src.PaperVersions.Count)); 
                cfg.CreateMap<PaperCreateViewModel, PaperDTO>();
                cfg.CreateMap<PaperDTO, PaperEditViewModel>();
                cfg.CreateMap<PaperEditViewModel, PaperDTO>();
                cfg.CreateMap<PaperDTO, PaperDetailsViewModel>();
                cfg.CreateMap<PaperDTO, PaperDeleteViewModel>();
                cfg.CreateMap<PaperDTO, MyPaperEditViewModel>();
                cfg.CreateMap<MyPaperEditViewModel, PaperDTO>(); 
                cfg.CreateMap<PaperDTO, MyPaperDetailsViewModel> ();

                //<IEnumerable<PaperShortDTO>>
                //PaperVersion
                cfg.CreateMap<PaperVersion, PaperVersionDTO>();
                cfg.CreateMap<PaperVersion, PaperVersionShortDTO>();
                cfg.CreateMap<PaperVersionDTO, PaperVersion>();

                cfg.CreateMap<PaperVersionDTO, PaperVersionDeleteViewModel>();

                //Reviews
                cfg.CreateMap<Review, ReviewShortDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<ReviewDTO, Review>();
                cfg.CreateMap<ReviewDTO, AddReviewViewModel>();
                cfg.CreateMap<AddReviewViewModel, ReviewDTO>();
                cfg.CreateMap<ReviewDTO, ReviewDetailsViewModel>();
                cfg.CreateMap<ReviewDTO, ReviewViewModel>(); 
                cfg.CreateMap<ReviewDTO, PaperVersionsRejectViewModel>();

            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");
        }
    }
}

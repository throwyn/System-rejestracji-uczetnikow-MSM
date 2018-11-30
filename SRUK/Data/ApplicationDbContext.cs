using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SRUK.Entities;
using SRUK.Models;

namespace SRUK.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Paper>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");

            builder.Entity<PaperVersion>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Review>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Season>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Participancy>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()");
        }
        
        public DbSet<SRUK.Entities.Season> Season { get; set; }
        public DbSet<SRUK.Entities.Paper> Paper { get; set; }
        public DbSet<SRUK.Entities.PaperVersion> PaperVerison { get; set; }
        public DbSet<SRUK.Entities.Review> Review { get; set; }
        public DbSet<SRUK.Entities.Participancy> Participancy { get; set; }
    }
}

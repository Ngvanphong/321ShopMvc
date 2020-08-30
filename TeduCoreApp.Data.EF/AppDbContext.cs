using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;
using TeduCoreApp.Data.EF.Configurations;
using TeduCoreApp.Data.EF.Extensions;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Interfaces;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

      
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }


        public DbSet<Contact> Contacts { set; get; }

        public DbSet<Footer> Footers { set; get; }

        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }

        public DbSet<Slide> Slides { set; get; }


        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<PageImage> PageImages { get; set; }






        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity Config

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            #endregion Identity Config

            // builder.Entity<BlogTag>().HasKey(x => new { x.TagId, x.BlogId });
            // builder.Entity<ProductTag>().HasKey(x => new { x.TagId, x.ProductId });
           

            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterConfiguration());               
            builder.AddConfiguration(new SystemConfigConfiguration());

            builder.AddConfiguration(new FunctionConfiguration());




        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                 .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var conectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(conectionString);
            return new AppDbContext(builder.Options);
        }
    }
}

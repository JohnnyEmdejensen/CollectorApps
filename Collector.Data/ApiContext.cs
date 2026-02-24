using Microsoft.EntityFrameworkCore;
using System;

namespace Collector.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("Created") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("Created").IsModified = false;
                    entry.Property("Updated").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Entity.Catalog> Catalogs { get; set; }
        public DbSet<Entity.Category> Categories { get; set; }
        public DbSet<Entity.Coin> Coins { get; set; }
        public DbSet<Entity.Color> Colors { get; set; }
        public DbSet<Entity.Condition> Conditions { get; set; }
        public DbSet<Entity.Continent> Continents { get; set; }
        public DbSet<Entity.Country> Countries { get; set; }
        public DbSet<Entity.Location> Locations { get; set; }
        public DbSet<Entity.Perforation> Perforations { get; set; }
        public DbSet<Entity.PublishedCountry> PublishedCountries { get; set;}
        public DbSet<Entity.PublishedCountryCoinLink> PublishedCountriesLinks { get; set; } 
        public DbSet<Entity.PubllishedCountryCategoryLink> PubllishedCountryCategoryLinks { get; set; }
        public DbSet<Entity.Stamp> Stamps { get; set; }
        public DbSet<Entity.Stampimage> Stampimages { get; set; }
        public DbSet<Entity.StampNumber> Stampnumbers { get; set; }
        public DbSet<Entity.StampValue> Stampvalues { get; set; }
        public DbSet<Entity.Stock> Stocks { get; set; }
        public DbSet<Entity.Supplier> Suppliers { get; set; }
        public DbSet<Entity.Supplierdeal> Suppliersdeals { get; set;}
        public DbSet<Entity.Topic> Topics { get; set; }
        public DbSet<Entity.Watermark> Watermarkings { get; set;}

    }
}

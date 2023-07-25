using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ModelViewController.DataBase
{
    [NonController]
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("DbSettings.json").Build();
            var s = config.GetConnectionString("DefaultConnection");

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
               // создаем базу данных при первом обращении
        }
    }
}

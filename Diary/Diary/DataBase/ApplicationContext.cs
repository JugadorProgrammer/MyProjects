using Diary.DataBase;
using Microsoft.EntityFrameworkCore;

namespace noTanish.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Day> Day { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("DbSettings.json").Build();
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

    }
}

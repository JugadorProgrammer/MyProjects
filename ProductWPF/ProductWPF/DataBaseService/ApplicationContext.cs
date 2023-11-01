using Microsoft.EntityFrameworkCore;
using ProductWPF.DataBaseService.Models;
using System.Collections.Generic;

namespace ProductWPF.DataBaseService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
            base.OnConfiguring(optionsBuilder);
        }

    }
}

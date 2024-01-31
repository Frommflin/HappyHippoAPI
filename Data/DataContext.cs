using HappyHippoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyHippoAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Quote> Quotes => Set<Quote>();
    }
}

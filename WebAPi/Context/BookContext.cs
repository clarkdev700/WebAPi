using Microsoft.EntityFrameworkCore;
using WebAPi.Entities;

namespace WebAPi.Context
{
    public class BookContext : DbContext
    {

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

    }
}

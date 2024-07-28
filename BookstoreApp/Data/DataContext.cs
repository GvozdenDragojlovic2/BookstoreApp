using BookstoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}

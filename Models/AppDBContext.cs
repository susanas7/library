using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Models
{
    public class AppDBContext : DbContext

    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Library.Models.Book> Books { get; set; } = default!;
        public DbSet<Library.Models.Review> Reviews { get; set; } = default!;

    }
}
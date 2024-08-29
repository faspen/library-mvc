using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Models
{
    public class LibraryMVCDbContext : DbContext
    {
        public LibraryMVCDbContext(DbContextOptions<LibraryMVCDbContext> options) : base(options)
        {
            
        }
    }
}
using Microsoft.EntityFrameworkCore;
using TravianMapWebAPI.Entities;

namespace TravianMapWebAPI.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Tile> Tiles { get; set; }
    }
}

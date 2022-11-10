using CELSIS.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CELSIS.Api.Data
{
    public class CelsisDbContext : DbContext
    {
        public DbSet<PlaceRating> PlaceRatings { get; set; }

        public DbSet<RouteRating> RouteRatings { get; set; }

        public CelsisDbContext(DbContextOptions<CelsisDbContext> options)
            : base(options)
        {
        }
    }
}

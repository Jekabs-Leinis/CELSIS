using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CELSIS.Api.Data.Entities
{
    public class RouteRating
    {
        [Key]
        public string GoogleRouteHash { get; set; }

        public long RatingCount { get; set; }
    
        public float Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

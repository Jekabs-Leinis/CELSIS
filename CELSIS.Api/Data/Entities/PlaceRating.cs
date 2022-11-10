using System.ComponentModel.DataAnnotations;

namespace CELSIS.Api.Data.Entities
{
    public class PlaceRating
    {
        [Key]
        public string GooglePlaceId { get; set; }

        public long RatingCount { get; set; }

        public float Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

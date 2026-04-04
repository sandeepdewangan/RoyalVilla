using System.ComponentModel.DataAnnotations;

namespace RoyalVillaAPI.Data.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        public required string Name { get; set; }
        public string? Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string? ImageUrl { get; set; }
    }
}

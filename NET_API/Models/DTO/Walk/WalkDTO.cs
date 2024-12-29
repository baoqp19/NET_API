namespace NET_API.Models.DTO.Walk;

using NET_API.Models.DTO.Difficulty;
using NET_API.Models.DTO.Region;

public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid Region { get; set; }
        public Guid Difficulty { get; set; }
}


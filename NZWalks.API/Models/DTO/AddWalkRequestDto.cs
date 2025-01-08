using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }
        public Guid RegionId { get; set; }


        //Navigation properties to link the classes by id same as database foreign key
        //public Difficulty Difficulty { get; set; }
        //public Region Region { get; set; }
    }
}

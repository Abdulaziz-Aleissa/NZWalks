using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
namespace NZWalks.API.auto_map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<River,RiverDto>().ReverseMap();
            CreateMap<River,AddRiverRequestDto>().ReverseMap();
            CreateMap<River, UpdateRiverRequestDto>().ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<Difficulty, AddDifficultyRequestDto>().ReverseMap();
            CreateMap<Difficulty, UpdateDifficultyRequestDto>().ReverseMap();



        }
    }
}

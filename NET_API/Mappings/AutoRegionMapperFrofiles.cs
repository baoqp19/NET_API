using AutoMapper;
using NET_API.Models.Domain;
using NET_API.Models.DTO.Difficulty;
using NET_API.Models.DTO.Region;
using NET_API.Models.DTO.Walk;

namespace NET_API.Mappings
{
    public class AutoRegionMapperFrofiles : Profile
    {

        public AutoRegionMapperFrofiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegion, Region>().ReverseMap();

            CreateMap<AddWalkDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();


        }

    }
}

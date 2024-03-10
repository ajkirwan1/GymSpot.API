using AutoMapper;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;

namespace GymSpot.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();


            /*CreateMap<UserDTO, User>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName));*/

        }


    }
}

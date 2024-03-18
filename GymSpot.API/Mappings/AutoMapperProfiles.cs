using AutoMapper;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;
using GymSpot.API.Models.DTOs.UserDTOs;

namespace GymSpot.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            // Regions mappings
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();

            /*CreateMap<UserDTO, User>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName));*/

            // Users mappings
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<AddUserRequestDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();
        }


    }
}

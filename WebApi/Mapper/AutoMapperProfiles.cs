using AutoMapper;
using Library.Models.Dtos;
using Library.Models;

namespace WebApi.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping cho AddUserRequestDto và User
            CreateMap<AddUserRequestDto, User>().ReverseMap();

            // Mapping cho UpdateUserRequestDto và User
            CreateMap<UpdateUserRequestDto, User>().ReverseMap();

            // Mapping cho User và UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CampusName, opt => opt.MapFrom(src => src.Campus.CampusName))  // Mapping CampusName từ Campus
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRole.RoleName));  // Mapping RoleName từ Role

            // Mapping cho Campus và CampusDto
            CreateMap<Campus, CampusDto>().ReverseMap();

            // Mapping cho UserRole và UserRoleDto
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
        }
    }
}

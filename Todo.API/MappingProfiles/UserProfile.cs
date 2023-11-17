using AutoMapper;
using TodoApp.API.ViewModels;
using TodoApp.Application.Common.DTO;
using TodoApp.Domain.Entities;

namespace TodoApp.API.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserDto, UserVm>().ReverseMap();
        }
    }
}

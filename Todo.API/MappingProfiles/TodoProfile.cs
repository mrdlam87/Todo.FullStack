using AutoMapper;
using TodoApp.API.ViewModels;
using TodoApp.Application.Common.DTO;
using TodoApp.Domain.Entities;

namespace TodoApp.API.MappingProfiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoDto, Todo>().ReverseMap();
            CreateMap<TodoDto, TodoVm>().ReverseMap();
        }
    }
}

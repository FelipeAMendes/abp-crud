using Abp.Crud.Entities;
using Abp.Crud.Models;
using AutoMapper;

namespace Abp.Crud;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskList, TaskListModel>();
        CreateMap<Person, PersonModel>();
    }
}

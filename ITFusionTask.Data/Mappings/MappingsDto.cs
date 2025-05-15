using AutoMapper;
using AutoMapper.Configuration;
using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Dtos.API_Dtos;
using ITFusionTask.Data.Entities;

namespace ITFusionTask.Data.Mappings
{
    public class DTOMappings : Profile
    {
        public DTOMappings()
        {
            
            CreateMap<Employee, EmployeeReturnDto>()
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.Name, c => c.MapFrom(x => x.E_Name))
                .ForMember(x => x.Phone, c => c.MapFrom(x => x.E_Phone))
                .ForMember(x => x.Salary, c => c.MapFrom(x => x.E_Salary))
                .ForMember(x => x.Gender, c => c.MapFrom(x => x.Gender.G_Name))
                .ForMember(x => x.JoinDate, c => c.MapFrom(x => x.E_JoinDate.ToString("dd-MMM-yyyy")))
                .ReverseMap();

            CreateMap<EmployeesDatum, Employee>()
                .ForMember(x => x.E_Name, c => c.MapFrom(x => x.E_Name))
                .ForMember(x => x.E_Phone, c => c.MapFrom(x => x.E_Phone))
                .ForMember(x => x.E_Salary, c => c.MapFrom(x => x.E_Salary))
                .ForMember(x => x.E_GenderId, c => c.MapFrom(x => x.E_Gender == "M" ? 1 : 2))
                .ForMember(x => x.E_JoinDate, c => c.MapFrom(x => x.E_JoinDate))
                .ReverseMap();
        }
    }
}

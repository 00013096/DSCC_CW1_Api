using AutoMapper;
using DSCC_CW1_Api.Dto;
using DSCC_CW1_Api.Models;

namespace DSCC_CW1_Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}

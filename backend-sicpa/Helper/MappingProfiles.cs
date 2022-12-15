using AutoMapper;
using backend_sicpa.Models;
using backend_sicpa.Dto;

namespace backend_sicpa.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Enterprise, EnterpriseDto>();
            CreateMap<EnterpriseDto, Enterprise>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<DepartmentsEmployee, DepartmentEmployeeDto>();
            CreateMap<DepartmentEmployeeDto, DepartmentsEmployee>();


        }

    }
}

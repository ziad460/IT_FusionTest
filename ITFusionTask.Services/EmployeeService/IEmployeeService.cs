using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Dtos.API_Dtos;
using ITFusionTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeByName(string Name);
        Task<Employee?> GetEmployeeByID(int Id);
        Task<int> UpdateListEmployees(List<Employee> employees);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<int> AddListEmployees(List<Employee> employees);
        Task<int> RemoveListEmployees(List<Employee> employees);
        Task<Employee> RemoveEmployee(Employee employee);
        Task<List<Genders>> GetAllGenders();
        Task<ReturnModelDto> AddListEmployeesAPI(EmployeesRequestRoot? requestDto);
        Task<PagingDto<EmployeeReturnDto>> GetAllEmployeesPaging(int skip, int take, string? search = null);
        Task<PagingDto<EmployeeReturnDto>> GetEmployeeFromAPI(int skip, int take, string? search = null);
        Task<ReturnModelDto> RemoveEmployeeAPI(int Id);
        Task<ReturnModelDto> EditEmployeeAPI(Employee employee);
    }
}

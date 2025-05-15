using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Reposatories.EmployeeReposatory
{
    public interface IEmployeeRepo
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeByName(string Name);
        Task<Employee?> GetEmployeeByID(int Id);
        Task<int> UpdateListEmployees(List<Employee> employees);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<int> AddListEmployees(List<Employee> employees);
        Task<List<Genders>> GetAllGenders();
        Task<PagingDto<Employee>> GetAllEmployeesPaging(int skip, int take, string? search = null);
    }
}

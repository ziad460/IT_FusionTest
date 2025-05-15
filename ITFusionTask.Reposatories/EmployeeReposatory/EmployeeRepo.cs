using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Entities;
using ITFusionTask.Data.UnityOfWrok;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Reposatories.EmployeeReposatory
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly IUnityOfWork _unityOfWork;
        private IBaseRepository<Employee> _baseReposatory { get; set; }
        public EmployeeRepo(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
            _baseReposatory = _unityOfWork.GetRepository<Employee>();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _baseReposatory.AddOne(employee);
            await _unityOfWork.CompleteAsync();
            return employee;
        }

        public async Task<int> AddListEmployees(List<Employee> employees)
        {
            _baseReposatory.AddList(employees);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _baseReposatory.UpdateOne(employee);
            await _unityOfWork.CompleteAsync();
            return employee;
        }

        public async Task<int> UpdateListEmployees(List<Employee> employees)
        {
            _baseReposatory.UpdateList(employees);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<Employee?> GetEmployeeByID(int Id)
        {
            return await _baseReposatory.FindAsync(x => !x.IsDeleted && x.Id == Id, ["Gender"]);
        }

        public async Task<Employee?> GetEmployeeByName(string Name)
        {
            return await _baseReposatory.FindAsync(x => !x.IsDeleted && x.E_Name == Name, ["Gender"]);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _baseReposatory.FindAllAsync(x => !x.IsDeleted , ["Gender"]);
        }

        public async Task<List<Genders>> GetAllGenders()
        {
            return await _unityOfWork.GetRepository<Genders>().GetAllAsync();
        }


        public async Task<PagingDto<Employee>> GetAllEmployeesPaging(int skip, int take, string? search = null)
        {
            List<Employee> Items = new();
            int TotalCount = 0;

            var query = _baseReposatory.QueryableFind(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.E_Name.Contains(search) || x.E_Phone.Contains(search));

            TotalCount = await query.CountAsync();

            Items = await query.OrderByDescending(x => x.E_JoinDate).Skip(skip).Take(take)
                .Include(x => x.Gender)
                .ToListAsync();

            return new PagingDto<Employee>() { ListItems = Items, TotalCountRows = TotalCount };
        }

    }
}

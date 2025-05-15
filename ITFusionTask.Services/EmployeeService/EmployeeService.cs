using AutoMapper;
using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Dtos.API_Dtos;
using ITFusionTask.Data.Entities;
using ITFusionTask.Reposatories.EmployeeReposatory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employeeRepo.AddEmployee(employee);
        }

        public async Task<int> AddListEmployees(List<Employee> employees)
        {
            return await _employeeRepo.AddListEmployees(employees);
        }

        public async Task<PagingDto<EmployeeReturnDto>> GetAllEmployeesPaging(int skip, int take, string? search = null)
        {
            PagingDto<Employee> employees = await _employeeRepo.GetAllEmployeesPaging(skip, take, search);
            return new PagingDto<EmployeeReturnDto>()
            {
                TotalCountRows = employees.TotalCountRows,
                ListItems = _mapper.Map<List<EmployeeReturnDto>>(employees.ListItems),
            };
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeRepo.GetAllEmployees();
        }

        public async Task<List<Genders>> GetAllGenders()
        {
            return await _employeeRepo.GetAllGenders();
        }


        public async Task<Employee?> GetEmployeeByID(int Id)
        {
            return await _employeeRepo.GetEmployeeByID(Id);
        }

        public async Task<Employee?> GetEmployeeByName(string Name)
        {
            return await _employeeRepo.GetEmployeeByName(Name);
        }

        public async Task<Employee> RemoveEmployee(Employee employee)
        {
            employee.IsDeleted = true;
            return await _employeeRepo.UpdateEmployee(employee);
        }

        public async Task<int> RemoveListEmployees(List<Employee> employees)
        {
            employees.ForEach(x => x.IsDeleted = true);
            return await _employeeRepo.UpdateListEmployees(employees);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _employeeRepo.UpdateEmployee(employee);
        }

        public async Task<int> UpdateListEmployees(List<Employee> employees)
        {
            return await _employeeRepo.UpdateListEmployees(employees);
        }

        public async Task<ReturnModelDto> AddListEmployeesAPI(EmployeesRequestRoot? requestDto)
        {
            List<Employee> employees = _mapper.Map<List<Employee>>(requestDto.EmployeesData);

            int result = await _employeeRepo.AddListEmployees(employees);

            return new ReturnModelDto { IsSucceeded = true, Message = $"Successfully Add {result} From {employees.Count}" };
        }

        public async Task<PagingDto<EmployeeReturnDto>> GetEmployeeFromAPI(int skip, int take, string? search = null)
        {
            var body = System.Text.Json.JsonSerializer.Serialize(new { Skip = skip , Take  = take, search = search});

            var client = new HttpClient(); 
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7185/api/Employee/GetAllEmployees");
            request.Content = new StringContent(body, null, "application/json");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            var obj = JsonConvert.DeserializeObject<PagingDto<EmployeeReturnDto>>(json.ToString());
            return obj;
        }

        public async Task<ReturnModelDto> EditEmployeeAPI(Employee employee)
        {
            var body = System.Text.Json.JsonSerializer.Serialize(employee);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7185/api/Employee/EditEmployee");
            request.Content = new StringContent(body, null, "application/json");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            var obj = JsonConvert.DeserializeObject<ReturnModelDto>(json.ToString());
            return obj;
        }

        public async Task<ReturnModelDto> RemoveEmployeeAPI(int Id)
        {
            var employee = await _employeeRepo.GetEmployeeByID(Id);
            var body = System.Text.Json.JsonSerializer.Serialize(employee);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7185/api/Employee/RemoveEmployee");
            request.Content = new StringContent(body, null, "application/json");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            var obj = JsonConvert.DeserializeObject<ReturnModelDto>(json.ToString());
            return obj;
        }


    }
}

using ITFusionTask.API.Models;
using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Dtos.API_Dtos;
using ITFusionTask.Data.Entities;
using ITFusionTask.Services.EmployeeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITFusionTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost]
        [Route("InsertEmployees")]
        public async Task<IActionResult> InsertEmployeesRequest([FromBody] EmployeesRequestRoot? requestDto)
        {
            ReturnModelDto respnseModel = await _employeeService.AddListEmployeesAPI(requestDto);
            if (respnseModel.IsSucceeded)
            {
                return new JsonResult(new { IsSucceeded = true, respnseModel.Message});
            }

            return new JsonResult(new { IsSucceeded = false, Message = "An Issue Happened." });
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployeesData([FromBody] GetAllEmployeesRequestRoot? requestDto)
        {
            PagingDto<EmployeeReturnDto> result = await _employeeService.GetAllEmployeesPaging(requestDto.Skip, requestDto.Take, requestDto.search);

            return Ok(result);
        }

        [HttpPost]
        [Route("EditEmployee")]
        public async Task<IActionResult> EditEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return new JsonResult(new { IsSucceeded = true, Message = "Done." });
        }

        [HttpPost]
        [Route("RemoveEmployee")]
        public async Task<IActionResult> RemoveEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.RemoveEmployee(employee);
            return new JsonResult(new { IsSucceeded = true, Message = "Done." });
        }

    }
}

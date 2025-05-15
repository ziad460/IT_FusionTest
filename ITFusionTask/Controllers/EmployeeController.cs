using ITFusionTask.Data.Dtos;
using ITFusionTask.Data.Entities;
using ITFusionTask.Models;
using ITFusionTask.Services.EmployeeService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITFusionTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllEmployeePaging(DataTableAjaxPostModel model)
        {
            PagingDto<EmployeeReturnDto> employeepagingdto = await _employeeService.GetEmployeeFromAPI(model.start, model.length, model.search.value);

            return Json(new
            {
                model.draw,
                data = employeepagingdto.ListItems,
                recordsTotal = employeepagingdto.TotalCountRows,
                recordsFiltered = employeepagingdto.TotalCountRows,
            });
        }

        public async Task<IActionResult> EditEmployee(int Id)
        {
            var employee = await _employeeService.GetEmployeeByID(Id);
            ViewBag.Genders = new SelectList(await _employeeService.GetAllGenders(), "Id", "G_Name");
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            var result = await _employeeService.EditEmployeeAPI(employee);
            return View(employee);
        }

        public async Task<IActionResult> RemoveEmployee(int Id)
        {
            var result = await _employeeService.RemoveEmployeeAPI(Id);
            return RedirectToAction("Index");
        }

    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeesMvc.Models;
using EmployeesMvc.Services;

namespace EmployeesMvc.Controllers;

public class HomeController(IEmployeesService employeesService) : Controller
{
    private readonly IEmployeesService _employeesService = employeesService;
    
    public async Task<ActionResult> Index()
    {
        var employees = await _employeesService.GetAllAsync();
        return View(new EmployeeListVm(employees));
    }

    public ActionResult Employee() => PartialView();
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return View(new EmployeeDetailsVm("/Home/CreateEmployee", null));
        }
        try
        {
            var employee = await _employeesService.GetByIdAsync(id.Value);
            return View(new EmployeeDetailsVm("/Home/UpdateEmployee", employee));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEmployee([FromForm] EmployeeDto employee)
    {
        await _employeesService.CreateAsync(employee);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmployee([FromForm]Employee employee)
    {
        try
        {
            await _employeesService.UpdateAsync(employee);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        try
        {
            await _employeesService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
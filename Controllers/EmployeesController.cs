using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBackEnd.Data;
using WebApiBackEnd.Models;

namespace WebApiBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly WebApiBackEndDbContext _webApiBackEndDbContext;

        public EmployeesController(WebApiBackEndDbContext webApiBackEndDbContext)
        {
            _webApiBackEndDbContext = webApiBackEndDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _webApiBackEndDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.id = Guid.NewGuid();
            await _webApiBackEndDbContext.Employees.AddAsync(employeeRequest);
            await _webApiBackEndDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee =
            await _webApiBackEndDbContext.Employees.FirstOrDefaultAsync(X => X.id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _webApiBackEndDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.name = updateEmployeeRequest.name;
            employee.email = updateEmployeeRequest.email;
            employee.salary = updateEmployeeRequest.salary;
            employee.phone = updateEmployeeRequest.phone;
            employee.department = updateEmployeeRequest.department;

            await _webApiBackEndDbContext.SaveChangesAsync();

            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _webApiBackEndDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _webApiBackEndDbContext.Employees.Remove(employee);
            await _webApiBackEndDbContext.SaveChangesAsync();

            return Ok(employee);

        }

    }
}

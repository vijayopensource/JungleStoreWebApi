using JungleEntities.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeControllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController:Controller
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeContext.Employees;
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee([FromRoute] int id)
        {
            var employee = _employeeContext.Employees.Where(x=> x.Id==id).SingleOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            if (ModelState.IsValid== false)
            {
                return BadRequest(ModelState);
            }

            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }
    }
}

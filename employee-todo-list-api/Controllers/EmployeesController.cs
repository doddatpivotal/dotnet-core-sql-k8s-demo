using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using employee_todo_list_api.Models;
using employee_todo_list_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using employee_todo_list_api.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace employee_todo_list_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeTodoContext _context;

        private readonly ILogger<EmployeesController> logger;
        private readonly IMapper mapper;

        public EmployeesController(
            IMapper mapper,
            ILogger<EmployeesController> logger, 
            EmployeeTodoContext context)
        {
            this._context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            var result = mapper.Map<List<EmployeeDTO>, IEnumerable<EmployeeViewModel>>(employees);
            return result;
        }

        // GET: api/Employees/id
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var foundEmployee = await _context.Employees.FindAsync(id);
            if (foundEmployee == null)
            {
                this.logger.LogInformation("can't find employee");
                return StatusCode(500);
            }
            return Ok(mapper.Map<EmployeeViewModel>(foundEmployee));
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeRecord([FromBody] EmployeeDTO newEmployeeDetails)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            _context.Employees.Add(newEmployeeDetails);
            
            var createCount = await _context.SaveChangesAsync();

            this.logger.LogInformation("createCount=" + createCount.ToString());

            if (createCount != 1)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT: api/Employees/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employee)
        {

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {


            var employeeItem = await _context.Employees.FindAsync(id);
            if (employeeItem == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeItem);
            await _context.SaveChangesAsync();

            return Ok();

        }

        private bool EmployeeItemExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

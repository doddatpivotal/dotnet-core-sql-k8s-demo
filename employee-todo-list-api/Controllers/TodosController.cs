using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using employee_todo_list_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using employee_todo_list_api.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace employee_todo_list_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly EmployeeTodoContext _context;

        private readonly ILogger<TodosController> logger;
        private readonly IMapper mapper;

        public TodosController(EmployeeTodoContext context,
            IMapper mapper,
            ILogger<TodosController> logger)
        {
            this.mapper = mapper;
            this.logger = logger;
            _context = context;
        }

        // GET: api/Todos
        [HttpGet("{employeeId}", Name = "GetEmployeeTodos")]
        public async Task<IEnumerable<TodoDTO>> GetEmployeeTodos(int employeeId)
        {

            var Todos = await Task.Run(() => _context.Todos.Where(t => t.EmployeeId == employeeId).ToList());


            return Todos;
        }

        // GET: api/Todos/id
        [HttpGet("{employeeId}/{id}", Name = "GetTodoById")]
        public async Task<IActionResult> GetTodoById(int employeeId, int id)
        {
            var foundTodo = await Task.Run(() => _context.Todos.Where(t => ((t.EmployeeId == employeeId) && (t.Id == id))).ToList());
            if (foundTodo == null)
            {
                return BadRequest();
            }
            return Ok(foundTodo);
        }

        // POST: api/Todos
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoDTO newTodo)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            this.logger.LogInformation("hello world");

            foreach (ModelError error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    this.logger.LogInformation(error.ErrorMessage);
                }

            _context.Todos.Add(newTodo);
            var isCreated = await _context.SaveChangesAsync();

            this.logger.LogInformation("createCount=" + isCreated.ToString());

            if (isCreated != 1)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT: api/Todos/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoDTO updatedTodo)
        {

            if (id != updatedTodo.Id)
            {
                return BadRequest();
            }


            _context.Entry(updatedTodo).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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
        public async Task<ActionResult> DeleteTodo(int id)
        {


            var todoItem = await _context.Todos.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoItemExists(long id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}

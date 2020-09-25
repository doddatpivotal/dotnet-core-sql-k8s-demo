using employee_todo_list_api.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_todo_list_api.Data
{
    public class EmployeeTodoContext : DbContext
    {
        public EmployeeTodoContext(DbContextOptions<EmployeeTodoContext> options) : base(options)
        {
        }

        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<TodoDTO> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDTO>().ToTable("Employee");
            modelBuilder.Entity<TodoDTO>().ToTable("Todo");
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace employee_todo_list_api.Models
{
    public class EmployeeDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public string Department { get; set; }
    }
}

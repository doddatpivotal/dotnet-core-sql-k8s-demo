using System;
using System.ComponentModel.DataAnnotations;
using employee_todo_list_api.Converters;
using System.Text.Json.Serialization;


namespace employee_todo_list_api.Models
{
    public class TodoDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonConverter(typeof(IntToStringConverter))]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public PriorityStatus Priority { get; set; }

        [Required]
        public States State { get; set; }

        public DateTime Estimate { get; set; }
    }
}

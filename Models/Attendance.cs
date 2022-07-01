using System.ComponentModel.DataAnnotations;
namespace CheckIn.Models
{
    public class Attendance
    { 
        public int Id { get; set; }
        [Display (Name = "Entrada" )]
        public DateTime? Entrance { get; set; }
        [Display(Name = "Salida")]
        public DateTime? Exit { get; set; }
        [Display(Name = "ID del Empleado")]
        public int? EmployeeId { get; set; }
        [Display(Name = "Empleado")]
        public Employee Employee { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckIn.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Correo")]
        public string Email { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Edad")]
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - BirthDate.Year;
                if (now < BirthDate.AddYears(age))
                    return --age;
                else
                    return age;
            }
        }
        public List<Attendance> Attendances { get; set; }
        public List<Department> Departments { get; set; }
        public List<Documentation> Documentations { get; set; }
        public List<Vacation> Vacations { get; set; }
    }
}

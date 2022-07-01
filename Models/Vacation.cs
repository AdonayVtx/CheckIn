namespace CheckIn.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

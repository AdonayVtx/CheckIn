namespace CheckIn.Models
{
    public class Documentation
    {
        public int Id { get; set; }
        public string NSS { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

namespace WebAdminTemplate.Model
{
    public class EmployeeGPF
    {
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Org { get; set; }
        public string Designation { get; set; }
        public decimal MonthlyContribution { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal InterestRate { get; set; }
        public decimal GPFAmount { get; set; }
        public decimal TotalGPF { get; set; }
    }
}

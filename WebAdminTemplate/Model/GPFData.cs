using System.ComponentModel.DataAnnotations;

namespace WebAdminTemplate.Model
{
    public class GPFData
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        [Required]
        public DateTime ProcessingDate { get; set; }
        [Required]
        public string? FilePath { get; set; }
        public decimal IntrestRate { get; set; }
        [Required]
        public string EmployeeType { get; set; }
        public decimal MemberContribution { get; set; }
        public decimal GPFAmount { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.Now;
    }
}

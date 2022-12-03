using System.ComponentModel.DataAnnotations;

namespace WebAdminTemplate.Model
{
    public class GPFModel
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime ProcessingDate { get; set; }
        [Required]
        public IFormFile FileData { get; set; }
        public decimal IntrestRate { get; set; }
        [Required]
        public string EmployeeType { get; set; }
        public string FilePath { get; set; }
    }
}

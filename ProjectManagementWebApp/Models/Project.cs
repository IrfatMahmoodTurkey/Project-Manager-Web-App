using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Name is Required")]
        [StringLength(50, ErrorMessage = "Length must be 50 characters long")]
        public string CodeName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Start Date is Required")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "End Date is Required")]
        public string EndDate { get; set; }
        [Required(ErrorMessage = "Status is Required")]
        public string Status { get; set; }
        public int State { get; set; }
        public List<File> Files { get; set; }
        public List<AssignProject> AssignProjects { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

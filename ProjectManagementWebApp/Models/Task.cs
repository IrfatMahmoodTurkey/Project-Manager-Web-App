using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        [Required(ErrorMessage = "Project is Required")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [ForeignKey("ToUser")]
        [Required(ErrorMessage = "User is Required")]
        public int ToUserId { get; set; }
        [ForeignKey("ByUser")]
        public int ByUserId { get; set; }
        public User ToUser { get; set; }
        public User ByUser { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public string DueDate { get; set; }
        [Required(ErrorMessage = "Priority is Required")]
        public string Priority { get; set; }
        public int State { get; set; }
        public int Seen { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

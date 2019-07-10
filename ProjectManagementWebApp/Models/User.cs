using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectManagementWebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Status is Required")]
        public string Status { get; set; }
        [ForeignKey("Designation")]
        [Required(ErrorMessage = "Designation is Required")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public int State { get; set; }
        public List<AssignProject> AssignProjects { get; set; }
        [InverseProperty("ByUser")]
        public List<Task> TasksBy { get; set; }
        [InverseProperty("ToUser")]
        public List<Task> TasksTo { get; set; }
        public List<Comment> Comments { get; set; }
        public string ProfileUrl { get; set; }
        public List<UserAccess> UserAccesses { get; set; }
    }
}

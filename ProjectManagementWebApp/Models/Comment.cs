using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Required(ErrorMessage = "Task Required")]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "Comment Required")]
        public string CommentDescription { get; set; }
        public string Date { get; set; }
        public int State { get; set; }
        public int Seen { get; set; }
        [Required(ErrorMessage = "Mension someone")]
        public string Mension { get; set; }
    }
}

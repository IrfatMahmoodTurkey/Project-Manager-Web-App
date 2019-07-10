using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CommentDescription { get; set; }
        public string Date { get; set; }
        public int Seen { get; set; }
        public string Mension { get; set; }
    }
}

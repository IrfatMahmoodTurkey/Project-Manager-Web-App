using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ToUserId { get; set; }
        public int ByUserId { get; set; }
        public string Description { get; set; }
        public string ToUser { get; set; }
        public string ByUser { get; set; }
        public string Priority { get; set; }
        public string DueDate { get; set; }
        public int Seen { get; set; }
    }
}

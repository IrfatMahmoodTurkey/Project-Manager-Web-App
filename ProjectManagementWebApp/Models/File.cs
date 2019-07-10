using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class File
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileNameWithExtension { get; set; }
        public string Url { get; set; }
        public int State { get; set; }
    }
}

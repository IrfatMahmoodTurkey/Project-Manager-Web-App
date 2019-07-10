using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Designation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Designation Required")]
        public string DesignationName { get; set; }
        public int State { get; set; }
        public List<User> Users { get; set; }
    }
}

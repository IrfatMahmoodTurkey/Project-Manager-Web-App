using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models.ViewModels
{
    public class UserAccessViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> PageId { get; set; }
        public int State { get; set; }
    }
}

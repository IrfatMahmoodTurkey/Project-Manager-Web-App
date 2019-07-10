using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models.ViewModels
{
    public class AssignedUserViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string UserDesignation { get; set; }
    }
}

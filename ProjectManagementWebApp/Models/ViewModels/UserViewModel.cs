using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int State { get; set; }
    }
}

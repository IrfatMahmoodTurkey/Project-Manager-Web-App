using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.Context;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Gateway.Repositories
{
    public class AssignedUserRepository:Repository<AssignProject>, IAssignUserRepository
    {
        private ApplicationDbContext context;

        public AssignedUserRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        // get assigned users
        public List<AssignedUserViewModel> GetAssignedUsers()
        {
            var query = context.AssignProjects.Include(x => x.User.Designation).Include(x=>x.Project).Where(x=>x.State == 1);

            List<AssignedUserViewModel> assignedUserViewModels = new List<AssignedUserViewModel>();

            foreach (var data in query)
            {
                AssignedUserViewModel model = new AssignedUserViewModel();

                model.Id = data.Id;
                model.ProjectName = data.Project.Name;
                model.UserName = data.User.Name;
                model.UserDesignation = data.User.Designation.DesignationName;

                assignedUserViewModels.Add(model);
            }

            return assignedUserViewModels;
        }

        // get assigned users by project id
        public List<User> GetAssignedUserByProjectId(int projectId)
        {
            var query = context.AssignProjects.Include(x => x.User)
                .Where(x => x.ProjectId == projectId && x.State == 1);

            List<User> users = new List<User>();
            foreach (var result in query)
            {
                User user = new User();

                user.Id = result.User.Id;
                user.Name = result.User.Name;
                user.Email = result.User.Email;

                users.Add(user);
            }

            return users;
        }
    }
}

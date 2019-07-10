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
    public class UserRepository: Repository<User>, IUserRepository
    {
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public int RowCount()
        {
            return context.Users.ToList().Count;
        }

        public List<UserViewModel> GetAllUsers()
        {
            var query = context.Users.Include(x => x.Designation).Where(x => x.State == 1).ToList();

            List<UserViewModel> viewModels = new List<UserViewModel>();

            foreach (var result in query)
            {
                UserViewModel user = new UserViewModel();

                user.Id = result.Id;
                user.Name = result.Name;
                user.Email = result.Email;
                user.Password = result.Password;
                user.DesignationId = result.DesignationId;
                user.Status = result.Status;
                user.ProfilePictureUrl = result.ProfileUrl;
                user.DesignationName = result.Designation.DesignationName;

                viewModels.Add(user);
            }

            return viewModels;
        }
    }
}

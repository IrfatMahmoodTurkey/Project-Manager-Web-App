using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        int RowCount();
        List<UserViewModel> GetAllUsers();
    }
}

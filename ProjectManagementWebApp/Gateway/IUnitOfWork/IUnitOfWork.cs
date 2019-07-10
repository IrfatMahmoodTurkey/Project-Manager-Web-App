using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Gateway.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IDesignationRepository Designation { get; set; }
        IUserRepository User { get; set; }
        IProjectRepository Project { get; set; }
        IRepository<File> File { get; set; }
        IAssignUserRepository AssignProject { get; set; }
        ITaskRepository Tasks { get; set; }
        ICommentRepository Comment { get; set; }
        IRepository<UserAccess> UserAccess { get; set; }
    }
}

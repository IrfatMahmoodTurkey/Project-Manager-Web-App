using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Gateway.Repositories;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.Context;

namespace ProjectManagementWebApp.Gateway.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork.IUnitOfWork
    {
        public IDesignationRepository Designation { get; set; }
        public IUserRepository User { get; set; }
        public IProjectRepository Project { get; set; }
        public IRepository<File> File { get; set; }
        public IAssignUserRepository AssignProject { get; set; }
        public ITaskRepository Tasks { get; set; }
        public ICommentRepository Comment { get; set; }
        public IRepository<UserAccess> UserAccess { get; set; }

        ApplicationDbContext context = new ApplicationDbContext();

        public UnitOfWork()
        {
            Designation = new DesignationRepository(context);
            User = new UserRepository(context);
            Project = new ProjectRepository(context);
            File = new Repository<File>(context);
            AssignProject = new AssignedUserRepository(context);
            Tasks = new TaskRepository(context);
            Comment = new CommentRepository(context);
            UserAccess = new Repository<UserAccess>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}

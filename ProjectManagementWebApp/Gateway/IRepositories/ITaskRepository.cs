using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagementWebApp.Models.ViewModels;
using Task = ProjectManagementWebApp.Models.Task;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface ITaskRepository:IRepository<Task>
    {
        int RowCount();
        List<TaskViewModel> GetTasksByProjectId(int projectId);
        int GetNewTasks(int userId);
        List<NewTaskViewModel> GetNewTasksList(int userId);
        int RowCountByProjectId(int projectId);
    }
}

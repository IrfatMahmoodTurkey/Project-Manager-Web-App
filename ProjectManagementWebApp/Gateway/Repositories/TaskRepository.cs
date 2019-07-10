using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Models.Context;
using ProjectManagementWebApp.Models.ViewModels;
using Task = ProjectManagementWebApp.Models.Task;

namespace ProjectManagementWebApp.Gateway.Repositories
{
    public class TaskRepository:Repository<Task>, ITaskRepository
    {
        private ApplicationDbContext context;
        public TaskRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public int RowCount()
        {
            int count = context.Tasks.Where(x => x.State == 1).ToList().Count;
            return count;
        }

        public List<TaskViewModel> GetTasksByProjectId(int projectId)
        {
            var query = context.Tasks.Include(x => x.ToUser).Include(x => x.ByUser)
                .Where(x => x.ProjectId == projectId && x.State == 1);

            List<TaskViewModel> viewModels = new List<TaskViewModel>();

            foreach (var data in query)
            {
                TaskViewModel viewModel = new TaskViewModel();

                viewModel.Id = data.Id;
                viewModel.ProjectId = data.ProjectId;
                viewModel.ToUserId = data.ToUserId;
                viewModel.ByUserId = data.ByUserId;
                viewModel.Description = data.Description;
                viewModel.ByUser = data.ByUser.Name;
                viewModel.ToUser = data.ToUser.Name;
                viewModel.Priority = data.Priority;
                viewModel.DueDate = data.DueDate;

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public int GetNewTasks(int userId)
        {
            return context.Tasks.Where(x => x.ToUserId == userId && x.State == 1 && x.Seen == 0).ToList().Count;
        }

        public List<NewTaskViewModel> GetNewTasksList(int userId)
        {
            var result = context.Tasks.Include(x => x.ByUser)
                .Where(x => x.ToUserId == userId && x.State == 1 && x.Seen == 0);

            List<NewTaskViewModel> newTasks = new List<NewTaskViewModel>();

            foreach (var data in result)
            {
                NewTaskViewModel task = new NewTaskViewModel();

                task.TaskId = data.Id;
                task.ProjectId = data.ProjectId;
                task.By = data.ByUser.Name;
                task.Priority = data.Priority;

                newTasks.Add(task);
            }

            return newTasks;
        }

        public int RowCountByProjectId(int projectId)
        {
            return context.Tasks.Where(x => x.ProjectId == projectId && x.State == 1).ToList().Count;
        }
    }
}

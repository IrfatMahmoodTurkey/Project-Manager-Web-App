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
    public class CommentRepository:Repository<Comment>, ICommentRepository
    {
        private ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        // get comments by projectId and taskId
        public List<CommentViewModel> GetCommentsByProjectAndTask(int projectId, int taskId)
        {
            var query = context.Comments.Include(x => x.User).Include(x => x.Project).Include(x => x.Task)
                .Where(x => x.ProjectId == projectId && x.TaskId == taskId && x.State == 1).OrderBy(x=>x.Date);

            List<CommentViewModel> comments = new List<CommentViewModel>();

            foreach (var data in query)
            {
                CommentViewModel viewModel = new CommentViewModel();

                viewModel.Id = data.Id;
                viewModel.ProjectId = data.ProjectId;
                viewModel.ProjectName = data.Project.Name;
                viewModel.TaskId = data.TaskId;
                viewModel.TaskName = data.Task.Description;
                viewModel.UserId = data.UserId;
                viewModel.UserName = data.User.Name;
                viewModel.CommentDescription = data.CommentDescription;
                viewModel.Date = data.Date;

                comments.Add(viewModel);

            }

            return comments;
        }

        public int RowCount(List<int> projectIds, string mention, int userId)
        {
            int count = 0;

            foreach (int projectId in projectIds)
            {
                int total = context.Comments.Where(x => x.ProjectId == projectId && x.UserId != userId && x.State == 1 && x.Seen == 0 && x.Mension == mention).ToList()
                    .Count;

                count = total + count;
            }

            return count;
        }

        public List<NewCommentViewModel> GetNewCommentList(List<int> projectIds, string mention, int userId)
        {
            List<NewCommentViewModel> commentViewModel = new List<NewCommentViewModel>();

            foreach (int projectId in projectIds)
            {
                var query = context.Comments.Include(x => x.Project).Include(x => x.User).Where(x => x.ProjectId == projectId && x.UserId != userId && x.State == 1 && x.Seen == 0 && x.Mension == mention).ToList();

                foreach (var data in query)
                {
                    NewCommentViewModel model = new NewCommentViewModel();

                    model.TaskId = data.Id;
                    model.ProjectId = data.ProjectId;
                    model.TaskId = data.TaskId;
                    model.ProjectName = data.Project.Name;
                    model.ByUserName = data.User.Name;

                    commentViewModel.Add(model);
                }
            }

            return commentViewModel;
        }
    }
}

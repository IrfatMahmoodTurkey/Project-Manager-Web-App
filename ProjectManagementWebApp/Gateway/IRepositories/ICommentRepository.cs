using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface ICommentRepository:IRepository<Comment>
    {
        List<CommentViewModel> GetCommentsByProjectAndTask(int projectId, int taskId);
        int RowCount(List<int> projectIds, string mention, int userId);
        List<NewCommentViewModel> GetNewCommentList(List<int> projectIds, string mention, int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Manager
{
    public class CommentManager
    {
        private UnitOfWork unitOfWork;

        public CommentManager()
        {
           unitOfWork = new UnitOfWork();
        }

        // add comment
        public string Save(Comment comment)
        {
            unitOfWork.Comment.Add(comment);
            int rowsAffected = unitOfWork.Complete();

            if (rowsAffected > 0)
            {
                return "1";
            }
            else
            {
                return Alert.AlertGenerate("Failed","Failed","Failed to Add Comment");
            }
        }

        // get comments by project id and taskid
        public List<CommentViewModel> GetCommentsByProjectIdAndTaskId(int projectId, int taskId)
        {
            return unitOfWork.Comment.GetCommentsByProjectAndTask(projectId, taskId);
        }

        // get comment by comment id
        public Comment GetCommentByCommentId(int id)
        {
            return unitOfWork.Comment.Find(x => x.Id == id && x.State == 1);
        }

        // comment update
        public string Update(Comment comment)
        {
            unitOfWork.Comment.Update(comment);
            int rowsAffected = unitOfWork.Complete();

            if (rowsAffected > 0)
            {
                return "1";
            }
            else
            {
                return Alert.AlertGenerate("Failed", "Failed", "Failed to Edit Comment");
            }
        }

        // check if comment exists or not
        public bool IsCommentExists(int id)
        {
            return unitOfWork.Comment.IsExists(x => x.Id == id && x.State == 1);
        }

        // make comment seen
        public void MakeCommentSeen(int projectId, int taskId, int userId)
        {
            string email = unitOfWork.User.Find(x => x.Id == userId && x.State == 1).Email;

            List<Comment> comments = unitOfWork.Comment.Get(x =>
                    x.ProjectId == projectId && x.TaskId == taskId && x.Seen == 0 &&
                    x.State == 1 && x.Mension == email)
                .ToList();

            if (comments != null)
            {
                List<Comment> makeSeenComment = new List<Comment>();

                foreach (Comment comment in comments)
                {
                    comment.Seen = 1;
                    makeSeenComment.Add(comment);
                }

                unitOfWork.Comment.UpdateRange(makeSeenComment);
                unitOfWork.Complete();
            }
        }
    }
}

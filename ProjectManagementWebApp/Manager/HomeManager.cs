using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Manager
{
    public class HomeManager
    {
        private UnitOfWork unitOfWork;

        public HomeManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public int NumberOfProjects()
        {
            return unitOfWork.Project.RowCount();
        }

        public int NumberOfTasks()
        {
            return unitOfWork.Tasks.RowCount();
        }

        public int NewTasks(int userId)
        {
            return unitOfWork.Tasks.GetNewTasks(userId);
        }

        public List<NewTaskViewModel> NewTaskList(int userId)
        {
            return unitOfWork.Tasks.GetNewTasksList(userId);
        }

        public List<int> GetProjectByUserId(int userId)
        {
            var query = unitOfWork.AssignProject.Get(x => x.UserId == userId && x.State == 1).ToList();
            List<int> projectIds = new List<int>();

            foreach (var project in query)
            {
                projectIds.Add(project.ProjectId);
            }

            IEnumerable<int> projectIdList = projectIds.Distinct();

            return projectIdList.ToList();
        }

        public int NumberOfNewComments(int userId)
        {
            string email = unitOfWork.User.FindById(userId).Email;
            return unitOfWork.Comment.RowCount(GetProjectByUserId(userId), email, userId);
        }

        public List<NewCommentViewModel> GetNewCommentList(int userId)
        {
            string email = unitOfWork.User.FindById(userId).Email;
            return unitOfWork.Comment.GetNewCommentList(GetProjectByUserId(userId), email, userId);
        }

        public List<int> GetProjectNumberByMonth()
        {
            return unitOfWork.Project.GetProjectCountByMonth();
        }

        public List<int> ThisAndLastTwoYearProjectNumber()
        {
            return unitOfWork.Project.NumberOfThisAndPrevYearProject();
        }
    }
}

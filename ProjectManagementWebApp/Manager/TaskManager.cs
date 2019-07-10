using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;
using Task = ProjectManagementWebApp.Models.Task;

namespace ProjectManagementWebApp.Manager
{
    public class TaskManager
    {
        private UnitOfWork unitOfWork;

        public TaskManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // add task
        public string Save(Task task)
        {
            Project project = unitOfWork.Project.Find(x=>x.Id == task.ProjectId && x.State == 1);
            
            DateTime projectEndDate = DateTime.ParseExact(project.EndDate, "MM/dd/yyyy", null);
            DateTime taskEndDate = DateTime.ParseExact(task.DueDate, "MM/dd/yyyy", null);

            DateTime startDate = DateTime.ParseExact(project.StartDate, "MM/dd/yyyy", null);

            if (projectEndDate > taskEndDate && startDate < taskEndDate)
            {
                unitOfWork.Tasks.Add(task);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return Alert.AlertGenerate("Success","Success","Task Added Successfully");
                }
                else
                {
                    return Alert.AlertGenerate("Falied", "Falied", "Falied to Add Task");
                }
            }
            else
            {
                return Alert.AlertGenerate("Falied", "Falied", "Invalid Date");
            }
        }

        // get tasks by project id
        public List<TaskViewModel> GetTasksByProjectId(int projectId)
        {
            return unitOfWork.Tasks.GetTasksByProjectId(projectId);
        }

        //get count tasks by project id
        public int GetCountOfTasksByProjectId(int projectId)
        {
            return unitOfWork.Tasks.RowCountByProjectId(projectId);
        }

        // get task by task id
        public Task GetTaskByTaskId(int id)
        {
            return unitOfWork.Tasks.Find(x=>x.Id == id && x.State == 1);
        }

        // update task
        public string Update(Task task)
        {
            Project project = unitOfWork.Project.Find(x=>x.Id == task.ProjectId && x.State == 1);

            DateTime projectEndDate = DateTime.ParseExact(project.EndDate,"MM/dd/yyyy", null);
            DateTime taskEndDate = DateTime.ParseExact(task.DueDate, "MM/dd/yyyy", null);

            DateTime startDate = DateTime.ParseExact(project.StartDate, "MM/dd/yyyy", null);

            if (projectEndDate > taskEndDate && startDate < taskEndDate)
            {
                unitOfWork.Tasks.Update(task);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return "1";
                }
                else
                {
                    return Alert.AlertGenerate("Falied", "Falied", "Failed to Edit Task");
                }
            }
            else
            {
                return Alert.AlertGenerate("Falied", "Failed", "Invalid Date");
            }
        }

        // get tasks by project id for comment dropdown
        public List<SelectListItem> GetTasksByProjectIdForDropDown(int projectId)
        {
            List<TaskViewModel> projects = GetTasksByProjectId(projectId);

            List<SelectListItem> selectListItems = projects.ConvertAll(x => new SelectListItem()
                {Text = x.Description, Value = x.Id.ToString()});

            return selectListItems;
        }

        // make task seen
        public void MakeSeenTask(int projectId, int userId)
        {
            List<Task> tasks =
                unitOfWork.Tasks.Get(x => x.ProjectId == projectId && x.ToUserId == userId && x.State == 1 && x.Seen == 0).ToList();
            
            if (tasks != null)
            { 
                List<Task> makeTaskSeen = new List<Task>();
                foreach (Task task in tasks)
                {
                    task.Seen = 1;
                    makeTaskSeen.Add(task);
                }

                unitOfWork.Tasks.UpdateRange(makeTaskSeen);
                unitOfWork.Complete();
            }
        }
    }
}

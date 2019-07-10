using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Manager
{
    public class AssignProjectManager
    {
        private UnitOfWork unitOfWork;

        public AssignProjectManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // assign project to user
        public string Save(AssignProject assignProject)
        {
            if (unitOfWork.AssignProject.IsExists(x =>
                x.UserId == assignProject.UserId && x.ProjectId == assignProject.ProjectId && x.State == 1))
            {
                return Alert.AlertGenerate("Falied", "Failed","Already Project Assigned to Another User");
            }
            else
            {
                unitOfWork.AssignProject.Add(assignProject);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return Alert.AlertGenerate("Success", "Success", "Project Assigned Successfully");
                }
                else
                {
                    return Alert.AlertGenerate("Falied", "Failed", "Failed to Assigned Project");
                }
            }
        }

        // get assigned user
        public List<AssignedUserViewModel> GetAssignedUsers()
        {
            return unitOfWork.AssignProject.GetAssignedUsers();
        }

        // remove assigned project
        public int Remove(int assignedId)
        {
            AssignProject assignProject = unitOfWork.AssignProject.Find(x=>x.Id == assignedId && x.State == 1);
            assignProject.State = 0;

            unitOfWork.AssignProject.Update(assignProject);
            int rowsAffected = unitOfWork.Complete();

            return rowsAffected;
        }

        // check project assigned or not
        public bool IsProjectAssigned(int assignedId)
        {
            return unitOfWork.AssignProject.IsExists(x => x.Id == assignedId && x.State == 1);
        }
    }
}

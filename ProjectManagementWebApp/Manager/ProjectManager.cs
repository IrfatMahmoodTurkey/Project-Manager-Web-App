using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Manager
{
    public class ProjectManager
    {
        private UnitOfWork unitOfWork;

        public ProjectManager()
        {
            unitOfWork = new UnitOfWork();
        }

        //save
        public string Save(Project project)
        {
            if (unitOfWork.Project.IsExists(x => x.CodeName == project.CodeName && x.State == 1))
            {
                return Alert.AlertGenerate("Failed", "Failed", "Same Project Code project Already Exists");
            }
            else
            {
                unitOfWork.Project.Add(project);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return Alert.AlertGenerate("Success", "Success", "New Project Saved and Uploaded Successfully");
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Failed", "Failed to Save New Project");
                }

            }
        }

        // get all projects
        public IEnumerable<Project> GetProjects()
        {
            return unitOfWork.Project.Get(x => x.State == 1);
        }

        // get by id
        public Project GetById(int id)
        {
            return unitOfWork.Project.Find(x=>x.Id == id && x.State == 1);
        }

        // update
        public string Update(Project project)
        {
            if (unitOfWork.Project.IsExists(x => x.Id != project.Id && x.CodeName == project.CodeName && x.State == 1))
            {
                return Alert.AlertGenerate("Failed", "Failed", "Same Project Code project Already Exists");
            }
            else
            {
                unitOfWork.Project.Update(project);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return "1";
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Failed", "Failed to Update Project");
                }
            }

        }

        // get project with files
        public Project GetProjectByProjectIdWithFiles(int id)
        {
            return unitOfWork.Project.GetProjectByProjectId(id);
        }

        // get project for dropdown
        public List<SelectListItem> GetProjectsForDropDown()
        {
            List<Project> projects = (List<Project>) unitOfWork.Project.Get(x => x.State == 1);

            List<SelectListItem> selectListItems =
                projects.ConvertAll(x => new SelectListItem() {Text = x.Name, Value = x.Id.ToString()});
            return selectListItems;
        }

        // check project exists or not
        public bool IsProjectExists(int projectId)
        {
            return unitOfWork.Project.IsExists(x => x.Id == projectId && x.State == 1);
        }
    }
}

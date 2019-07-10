using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Manager
{
    public class UserManager
    {
        private UnitOfWork unitOfWork;

        public UserManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // save
        public string Save(User user)
        {
            if (unitOfWork.User.IsExists(x => x.Email == user.Email))
            {
                return Alert.AlertGenerate("Failed","Already Exists", "User Email already exists");
            }
            else
            {
                unitOfWork.User.Add(user);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return Alert.AlertGenerate("Success", "Success", "Saved New User Successfully");
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Falied", "Falied to Save New User");
                }
            }
        }

        // get all
        public List<UserViewModel> GetAll()
        {
            return unitOfWork.User.GetAllUsers();
        }

        // get by id
        public User GetById(int id)
        {
            return unitOfWork.User.Find(x=>x.Id == id && x.State == 1);
        }

        //update
        public string Update(User user)
        {
            if (unitOfWork.User.IsExists(x => x.Id != user.Id && x.Email == user.Email && x.State == 1))
            {
                return Alert.AlertGenerate("Failed", "Already Exists", "User Email already exists");
            }
            else
            {
                unitOfWork.User.Update(user);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return "1";
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Failed", "Failed to Edit User");
                }
            }
        }

        /// get all users for assign projects dropdown
        public List<SelectListItem> GetUsersForDropDownForAssignProject()
        {
            int designationId = unitOfWork.Designation.Find(x => x.DesignationName == "Admin").Id;

            List<User> users = (List<User>) unitOfWork.User.Get(x => x.DesignationId != designationId && x.State == 1);

            List<SelectListItem> selectListItems =
                users.ConvertAll(x => new SelectListItem() {Text = x.Name, Value = x.Id.ToString()});

            return selectListItems;
        }

        // get users by project id
        public List<User> GetAssignedUserByProjectId(int projectId)
        {
            return unitOfWork.AssignProject.GetAssignedUserByProjectId(projectId);
        }

        // get assigned users for edit load
        public List<SelectListItem> GetAssignedUserDropDownForEdit(int projectId)
        {
            List<User> users = GetAssignedUserByProjectId(projectId);

            List<SelectListItem> selectListItems =
                users.ConvertAll(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            return selectListItems;
        }

        // get assigned users for comment
        public List<SelectListItem> GetAssignedUserDropDownForComment(int projectId)
        {
            List<User> users = GetAssignedUserByProjectId(projectId);

            List<SelectListItem> selectListItems =
                users.ConvertAll(x => new SelectListItem() { Text = x.Name+" - "+x.Email, Value = x.Email });
            return selectListItems;
        }

        // check user is exists or not
        public bool IsUserExists(int userId)
        {
            return unitOfWork.User.IsExists(x => x.Id == userId && x.State == 1);
        }
    }
}

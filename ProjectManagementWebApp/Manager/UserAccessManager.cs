using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Manager
{
    public class UserAccessManager
    {
        private UnitOfWork unitOfWork;

        public UserAccessManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // check user access
        public bool HasAccess(int userId, int pageId, int designationId)
        {
            if (unitOfWork.UserAccess.IsExists(x => x.UserId == userId && x.PageId == pageId && x.State == 1) ||
                unitOfWork.Designation.FindById(designationId).DesignationName.Equals("Admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // get given user access
        public UserAccessViewModel GetUserAccess(int userId)
        {
            List<UserAccess> result = unitOfWork.UserAccess.Get(x => x.UserId == userId && x.State == 1).ToList();

            UserAccessViewModel userAccessViewModel = new UserAccessViewModel();
            if (result.Count > 0)
            {
                userAccessViewModel.UserId = result[0].UserId;
                List<int> listOfPageId = new List<int>();

                foreach (var data in result)
                {
                   listOfPageId.Add(data.PageId);
                }
                userAccessViewModel.PageId = listOfPageId;

                return userAccessViewModel;
            }
            else
            {
                userAccessViewModel.Id = userId;
                return userAccessViewModel;
            }
        }

        // give access to user
        public int SaveOrUpdate(UserAccessViewModel userAccessViewModel)
        {
            if (unitOfWork.UserAccess.IsExists(x => x.UserId == userAccessViewModel.UserId))
            {
                List<UserAccess> userAccesses = unitOfWork.UserAccess
                    .Get(x => x.UserId == userAccessViewModel.UserId).ToList();

                unitOfWork.UserAccess.RemoveList(userAccesses);
                int removeRowsAffected = unitOfWork.Complete();

                if (removeRowsAffected > 0)
                {
                    List<UserAccess> userAccessesUpdated = new List<UserAccess>();

                    foreach (int pageId in userAccessViewModel.PageId)
                    {
                        UserAccess userAccess = new UserAccess();

                        userAccess.UserId = userAccessViewModel.UserId;
                        userAccess.PageId = pageId;
                        userAccess.State = userAccessViewModel.State;

                        userAccessesUpdated.Add(userAccess);
                    }

                    unitOfWork.UserAccess.AddRange(userAccessesUpdated);
                    int rowsAffected = unitOfWork.Complete();

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                List<UserAccess> userAccesses = new List<UserAccess>();

                foreach (int pageId in userAccessViewModel.PageId)
                {
                    UserAccess userAccess = new UserAccess();

                    userAccess.UserId = userAccessViewModel.UserId;
                    userAccess.PageId = pageId;
                    userAccess.State = userAccessViewModel.State;

                    userAccesses.Add(userAccess);
                }

                unitOfWork.UserAccess.AddRange(userAccesses);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // remove all access
        public int RemoveAllAccess(int usedId)
        {
            List<UserAccess> userAccesses = unitOfWork.UserAccess
                .Get(x => x.UserId == usedId && x.State == 1).ToList();

            List<UserAccess> newList = new List<UserAccess>();
            foreach (UserAccess userAccess in userAccesses)
            {
                userAccess.State = 0;

                newList.Add(userAccess);
            }

            unitOfWork.UserAccess.UpdateRange(userAccesses);
            int updated = unitOfWork.Complete();

            if (updated > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

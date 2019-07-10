using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;

namespace ProjectManagementWebApp.Manager
{
    public class UserAuthenticationManager
    {
        private UnitOfWork unitOfWork;

        public UserAuthenticationManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // check authentication
        public User CheckAuthentication(AuthenticationViewModel model)
        {
            User user = unitOfWork.User.Find(
                x => x.Email == model.Email && x.Password == model.Password && x.State == 1);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }
    }
}

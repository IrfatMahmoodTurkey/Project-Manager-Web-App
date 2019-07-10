using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using Newtonsoft.Json;
using ProjectManagementWebApp.FileUploadClass;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Controllers
{
    public class UserController : Controller
    {
        private UserManager userManager;
        private DesignationManager designationManager;
        private ProjectManager projectManager;
        private AssignProjectManager assignProjectManager;
        private UploadFile uploadFile;
        private UserAccessManager userAccess;

        public UserController()
        {
            userManager = new UserManager();
            designationManager = new DesignationManager();
            projectManager = new ProjectManager();
            assignProjectManager = new AssignProjectManager();
            uploadFile = new UploadFile();
            userAccess = new UserAccessManager();
        }

        // save
        // only for Admin
        [HttpGet]
        public IActionResult Save()
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, -20, user.DesignationId))
                {
                    ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                    return View();
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // only for Admin
        [HttpPost]
        public IActionResult Save(User user)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(userInfo.Id, -20, userInfo.DesignationId))
                {
                    if (ModelState.IsValid)
                    {
                        user.State = 1;
                        user.ProfileUrl = "http://www.project-manager.somee.com/ProfilePictures/avater.jpg";
                        ViewData["Message"] = userManager.Save(user);
                        ModelState.Clear();
                        ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                        return View();
                    }
                    else
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Warning", "Failed", "Fill up all fields correctly"); ;
                        ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                        return View(user);
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // view all
        // only for Admin
        [HttpGet]
        public IActionResult ViewAll()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, -20, user.DesignationId))
                {
                    ViewBag.Users = userManager.GetAll();
                    return View();
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "LogIn");
            }
        }

        // update
        // only for Admin
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(userInfo.Id, -20, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(id))
                    {
                        User user = userManager.GetById(id);
                        ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                        return View(user);
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // only for Admin
        [HttpPost]
        public IActionResult Edit(User user)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(userInfo.Id, -20, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(user.Id))
                    {
                        if (ModelState.IsValid)
                        {
                            user.State = 1;
                            string updated = userManager.Update(user);

                            if (updated.Equals("1"))
                            {
                                return RedirectToAction("ViewAll", "User");
                            }
                            else
                            {
                                ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                                ViewData["Message"] = updated;
                                return View(user);
                            }
                        }
                        else
                        {
                            ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                            ViewData["Message"] = Alert.AlertGenerate("Warning", "Failed", "Fill up all fields correctly"); ;
                            return View(user);
                        }
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // remove user
        // only for Admin
        public IActionResult Remove(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(userInfo.Id, -20, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(id))
                    {
                        User user = userManager.GetById(id);
                        user.State = 0;
                        userManager.Update(user);

                        return RedirectToAction("ViewAll", "User");
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }
        
        // assign project to user
        [HttpGet]
        public IActionResult AssignProject()
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 3, user.DesignationId))
                {
                    ViewBag.Users = userManager.GetUsersForDropDownForAssignProject();
                    ViewBag.Projects = projectManager.GetProjectsForDropDown();
                    ViewBag.AssignedUsers = assignProjectManager.GetAssignedUsers();
                    return View();
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult AssignProject(AssignProject assignProject)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 3, user.DesignationId))
                {
                    if (ModelState.IsValid)
                    {
                        assignProject.State = 1;
                        ViewData["Message"] = assignProjectManager.Save(assignProject);

                        ViewBag.Users = userManager.GetUsersForDropDownForAssignProject();
                        ViewBag.Projects = projectManager.GetProjectsForDropDown();
                        ViewBag.AssignedUsers = assignProjectManager.GetAssignedUsers();
                        return View();
                    }
                    else
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Failed","Failed","Fill up all fields correctly");

                        ViewBag.Users = userManager.GetUsersForDropDownForAssignProject();
                        ViewBag.Projects = projectManager.GetProjectsForDropDown();
                        ViewBag.AssignedUsers = assignProjectManager.GetAssignedUsers();
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // remove assigned project
        public IActionResult UnassignProject(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 3, user.DesignationId))
                {
                    if (assignProjectManager.IsProjectAssigned(id))
                    {
                        assignProjectManager.Remove(id);
                        return RedirectToAction("AssignProject", "User");
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // reset password
        public IActionResult ResetPassword(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(userInfo.Id, -20, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(id))
                    {
                        User user = userManager.GetById(id);
                        string email = user.Email;

                        user.Password = email + "123";

                        string updated = userManager.Update(user);

                        return RedirectToAction("ViewAll", "User");
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // change profile picture
        [HttpGet]
        public IActionResult ProfilePicture()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                ViewBag.PictureLink = userInfo.ProfileUrl;
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult ProfilePicture(IFormFile profilePicture)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (profilePicture != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + userInfo.Name + "_" + profilePicture.FileName;

                    uploadFile.UploadProfilePicture(profilePicture, fileName);

                    userInfo.ProfileUrl = "http://www.project-manager.somee.com/ProfilePictures/" +
                                          fileName;

                    string update = userManager.Update(userInfo);

                    if (update.Equals("1"))
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Success", "Success", "Profile Picture Uploaded Successfully");
                        ViewBag.PictureLink = userInfo.ProfileUrl;
                    }
                    else
                    {
                        ViewData["Message"] = update;
                        ViewBag.PictureLink = userInfo.ProfileUrl;
                    }

                    return View();
                }
                else
                {
                    ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Please Browse Picture to Upload");
                    ViewBag.PictureLink = userInfo.ProfileUrl;

                    return View();
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // change password
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);
                
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (ModelState.IsValid)
                {
                    string oldPassword = userInfo.Password;

                    if (oldPassword.Equals(changePassword.MainPassword))
                    {
                        if (changePassword.NewPassword.Equals(changePassword.ReNewPassword))
                        {
                            userInfo.Password = changePassword.NewPassword;

                            string updated = userManager.Update(userInfo);

                            if (updated.Equals("1"))
                            {
                                HttpContext.Session.SetString("userInfo", "");

                                return RedirectToAction("Login", "LogIn");
                            }
                            else
                            {
                                ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Failed to Change Password"); ;
                                return View();
                            }
                        }
                        else
                        {
                            ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "New and Re Entered Password does not matched"); ;
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Warning", "Failed", "Enter Your Old Password Correctly"); ;
                        return View();
                    }
                }
                else
                {
                    ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Fill up all fields correctly"); ;
                    return View();
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // give user access for Admin
        [HttpGet]
        public IActionResult UserAccess(int userId)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(userInfo.Id, -10, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(userId))
                    {
                        UserAccessViewModel userAccessViewModel = userAccess.GetUserAccess(userId);
                        ViewBag.UserId = userId;
                        return View(userAccessViewModel);
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult UserAccess(UserAccessViewModel userAccessViewModel)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User userInfo = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(userInfo.Id, -10, userInfo.DesignationId))
                {
                    if (userManager.IsUserExists(userAccessViewModel.UserId))
                    {
                        if (ModelState.IsValid)
                        {
                            if (userAccessViewModel.PageId == null)
                            {
                                int saved = userAccess.RemoveAllAccess(userAccessViewModel.UserId);

                                if (saved == 1)
                                {
                                    return RedirectToAction("ViewAll", "User");
                                }
                                else
                                {
                                    ViewBag.UserId = userAccessViewModel.UserId;
                                    ViewData["Message"] = saved;
                                    UserAccessViewModel userAccessViewModelReturn = userAccess.GetUserAccess(userAccessViewModel.UserId);
                                    return View(userAccessViewModelReturn);
                                }
                            }
                            else
                            {
                                userAccessViewModel.State = 1;
                                int saved = userAccess.SaveOrUpdate(userAccessViewModel);

                                if (saved == 1)
                                {
                                    return RedirectToAction("ViewAll", "User");
                                }
                                else
                                {
                                    ViewBag.UserId = userAccessViewModel.UserId;
                                    ViewData["Message"] = saved;
                                    UserAccessViewModel userAccessViewModelReturn = userAccess.GetUserAccess(userAccessViewModel.UserId);
                                    return View(userAccessViewModelReturn);
                                }
                            }

                        }
                        else
                        {
                            ViewBag.UserId = userAccessViewModel.UserId;
                            ViewData["Message"] = Alert.AlertGenerate("Failed","Falied","Fill up all fields correctly");
                            UserAccessViewModel userAccessViewModelReturn = userAccess.GetUserAccess(userAccessViewModel.UserId);
                            return View(userAccessViewModelReturn);
                        }
                    }
                    else
                    {
                        return NotFound("404 Not Found");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }
    }
}
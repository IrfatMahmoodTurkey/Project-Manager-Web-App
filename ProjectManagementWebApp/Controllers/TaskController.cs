using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Utility;
using Task = ProjectManagementWebApp.Models.Task;

namespace ProjectManagementWebApp.Controllers
{
    public class TaskController : Controller
    {
        private ProjectManager projectManager;
        private UserManager userManager;
        private TaskManager taskManager;
        private UserAccessManager userAccess;

        public TaskController()
        {
            projectManager = new ProjectManager();
            userManager = new UserManager();
            taskManager = new TaskManager();
            userAccess = new UserAccessManager();
        }

        // add task
        [HttpGet]
        public IActionResult Add()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 2, user.DesignationId))
                {
                    ViewBag.Projects = projectManager.GetProjectsForDropDown();
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
        public IActionResult Add(Task task)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 2, user.DesignationId))
                {
                    if (ModelState.IsValid)
                    {
                        task.ByUserId = user.Id;
                        task.State = 1;
                        task.Seen = 0;

                        ViewData["Message"] = taskManager.Save(task);
                    }
                    else
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Falied", "Falied", "Fill up all fields correctly"); ;
                    }

                    ViewBag.Projects = projectManager.GetProjectsForDropDown();
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

        // get assigned user by project id
        public JsonResult GetAssignedUserByProjectId(int projectId)
        {
            List<User> users = userManager.GetAssignedUserByProjectId(projectId);
            return Json(users);
        }

        // edit task
        [HttpGet]
        public IActionResult Edit(int id, int projectId)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 2, user.DesignationId))
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                    Task task = taskManager.GetTaskByTaskId(id);

                    if (task.ByUserId == userId)
                    {
                        ViewBag.Projects = projectManager.GetProjectsForDropDown();
                        ViewBag.Users = userManager.GetAssignedUserDropDownForEdit(task.ProjectId);
                        ViewData["ProjectId"] = projectId;

                        return View(task);
                    }

                    else
                    {
                        return NotFound();
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
        public IActionResult Edit(Task task, int projectId)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 2, user.DesignationId))
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                    if (userId == task.ByUserId)
                    {
                        if (ModelState.IsValid)
                        {
                            task.State = 1;
                            task.Seen = 0;
                            string updated = taskManager.Update(task);

                            if (updated.Equals("1"))
                            {
                                return RedirectToAction("Details", "Project", new { id = projectId });
                            }
                            else
                            {
                                ViewData["Message"] = updated;

                                ViewBag.Projects = projectManager.GetProjectsForDropDown();
                                ViewBag.Users = userManager.GetAssignedUserDropDownForEdit(userId);

                                return View(task);
                            }
                        }
                        else
                        {
                            ViewData["Message"] = Alert.AlertGenerate("Failed","Failed","Fill up all fields correctly");

                            ViewBag.Projects = projectManager.GetProjectsForDropDown();
                            ViewBag.Users = userManager.GetAssignedUserDropDownForEdit(userId);

                            return View(task);
                        }
                    }
                    else
                    {
                        return NotFound();
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

        // remove tasks
        public IActionResult Remove(int id, int projectId)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 2, user.DesignationId))
                {
                    Task task = taskManager.GetTaskByTaskId(id);
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                    if (task.ByUserId == userId)
                    {
                        task.State = 0;

                        taskManager.Update(task);
                        return RedirectToAction("Details", "Project", new { id = projectId });
                    }
                    else
                    {
                        return NotFound();
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
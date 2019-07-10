using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Controllers
{
    public class CommentController : Controller
    {
        private TaskManager taskManager;
        private CommentManager commentManager;
        private ProjectManager projectManager;
        private UserAccessManager userAccess;
        private UserManager userManager;

        public CommentController()
        {
            taskManager = new TaskManager();
            commentManager = new CommentManager();
            projectManager = new ProjectManager();
            userAccess = new UserAccessManager();
            userManager = new UserManager();
        }

        // add comment
        [HttpGet]
        public IActionResult Add(int projectId)
        {
            var authData = HttpContext.Session.GetString("userInfo");


            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                ViewBag.Tasks = taskManager.GetTasksByProjectIdForDropDown(projectId);
                ViewBag.Users = userManager.GetAssignedUserDropDownForComment(projectId);

                ViewData["ProjectId"] = projectId;

                return View();

            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult Add(Comment comment)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                if (ModelState.IsValid)
                {
                    comment.UserId = userId;
                    comment.State = 1;
                    comment.Seen = 0;
                    comment.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                    string saved = commentManager.Save(comment);

                    if (saved.Equals("1"))
                    {
                        return RedirectToAction("Details", "Project", new { id = comment.ProjectId });
                    }
                    else
                    {
                        ViewBag.Tasks = taskManager.GetTasksByProjectIdForDropDown(comment.ProjectId);
                        ViewBag.Users = userManager.GetAssignedUserDropDownForComment(comment.ProjectId);

                        ViewData["ProjectId"] = comment.ProjectId;
                        ViewData["Message"] = saved;

                        return View(comment);
                    }
                }
                else
                {
                    ViewBag.Tasks = taskManager.GetTasksByProjectIdForDropDown(comment.ProjectId);
                    ViewBag.Users = userManager.GetAssignedUserDropDownForComment(comment.ProjectId);

                    ViewData["ProjectId"] = comment.ProjectId;
                    ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Fill up all fields correctly");

                    return View(comment);
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }


        // view comments by task and project
        [HttpGet]
        public IActionResult ViewComments(int projectId, int taskId)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                commentManager.MakeCommentSeen(projectId, taskId, user.Id);
                List<CommentViewModel> comments = commentManager.GetCommentsByProjectIdAndTaskId(projectId, taskId);

                ViewData["ProjectName"] = projectManager.GetById(projectId).Name;
                ViewData["TaskName"] = taskManager.GetTaskByTaskId(taskId).Description;

                ViewBag.Comments = comments;
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // edit comment
        [HttpGet]
        public IActionResult Edit(int projectId, int taskId, int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (commentManager.IsCommentExists(id))
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                    Comment comment = commentManager.GetCommentByCommentId(id);

                    if (comment.UserId == userId)
                    {
                        return View(comment);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound("404 Not Found");
                }

            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (commentManager.IsCommentExists(comment.Id))
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                    if (comment.UserId == userId)
                    {
                        if (ModelState.IsValid)
                        {
                            comment.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                            comment.State = 1;
                            comment.Seen = 0;

                            string updated = commentManager.Update(comment);

                            if (updated.Equals("1"))
                            {
                                return RedirectToAction("ViewComments", "Comment",
                                    new { projectId = comment.ProjectId, taskId = comment.TaskId });
                            }
                            else
                            {
                                ViewData["Message"] = updated;
                                return View(comment);
                            }
                        }
                        else
                        {
                            ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Fill up all fields correctly"); ;
                            return View(comment);
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound("404 Not Found");
                }

            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // remove comment
        public IActionResult Remove(int projectId, int taskId, int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (commentManager.IsCommentExists(id))
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                    Comment comment = commentManager.GetCommentByCommentId(id);

                    if (comment.UserId == userId)
                    {
                        comment.State = 0;
                        commentManager.Update(comment);

                        return RedirectToAction("ViewComments", "Comment",
                            new { projectId = comment.ProjectId, taskId = comment.TaskId });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound("404 Not Found");
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
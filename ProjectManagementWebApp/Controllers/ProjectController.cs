using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementWebApp.FileUploadClass;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private UploadFile fileUpload;
        private ProjectManager projectManager;
        private FileManager fileManager;
        private TaskManager taskManager;
        private UserAccessManager userAccess;

        public ProjectController()
        {
            fileUpload = new UploadFile();
            projectManager = new ProjectManager();
            fileManager = new FileManager();
            taskManager = new TaskManager();
            userAccess = new UserAccessManager();
        }

        // add project
        [HttpGet]
        public IActionResult Add()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
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
        public IActionResult Add(Project project, List<IFormFile> files)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
                    if (ModelState.IsValid)
                    {
                        if (files.Count <= 0)
                        {
                            project.State = 1;
                            ViewData["Message"] = projectManager.Save(project);
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            DateTime startDate = DateTime.ParseExact(project.StartDate, "MM/dd/yyyy", null);
                            DateTime endDate = DateTime.ParseExact(project.EndDate, "MM/dd/yyyy", null);

                            if (endDate > startDate)
                            {
                                project.State = 1;
                                List<File> fileList = new List<File>();

                                foreach (IFormFile file in files)
                                {
                                    Models.File fileObj = new File();

                                    fileObj.FileName = file.FileName;
                                    fileObj.FileExtension = file.ContentType;
                                    fileObj.FileNameWithExtension = fileObj.FileName + "." + fileObj.FileExtension;
                                    fileObj.State = 1;

                                    string fileName = Guid.NewGuid().ToString() + "_" + fileObj.FileName;
                                    fileObj.Url = "http://www.project-manager.somee.com/ProjectFiles/" + fileName;

                                    fileUpload.UploadOnRemoteServer(file, fileName);
                                    fileList.Add(fileObj);
                                }

                                project.Files = fileList;

                                ViewData["Message"] = projectManager.Save(project);
                                ModelState.Clear();
                                return View();
                            }
                            else
                            {
                                ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Invalid Start and End Date");
                                return View(project);
                            }
                        }
                    }
                    else
                    {
                        ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Fill up all fields correctly");
                        return View(project);
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
        [HttpGet]
        public IActionResult ViewAll()
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                ViewBag.Projects = projectManager.GetProjects();
                return View();

            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");

                return RedirectToAction("Login", "LogIn");
            }
        }

        // edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
                    if (projectManager.IsProjectExists(id))
                    {
                        Project project = projectManager.GetById(id);
                        ViewBag.Files = fileManager.GetFileForDropDown(id);
                        return View(project);
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
        public IActionResult Edit(Project project, IFormFile file, string fileId)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
                    if (projectManager.IsProjectExists(project.Id))
                    {
                        if (ModelState.IsValid)
                        {
                            DateTime startDate = DateTime.ParseExact(project.StartDate, "MM/dd/yyyy", null);
                            DateTime endDate = DateTime.ParseExact(project.EndDate, "MM/dd/yyyy", null);

                            if (endDate > startDate)
                            {
                                if (fileId != null)
                                {
                                    project.State = 1;
                                    Models.File fileModel = fileManager.GetFileById(Convert.ToInt32(fileId));

                                    fileModel.FileName = file.FileName;
                                    fileModel.FileExtension = file.ContentType;
                                    fileModel.FileNameWithExtension =
                                        fileModel.FileName + "." + fileModel.FileExtension;

                                    string fileName = Guid.NewGuid().ToString() + "_" + fileModel.FileName;
                                    fileModel.Url = "http://www.project-manager.somee.com/ProjectFiles/" + fileName;

                                    List<File> files = new List<File>();
                                    fileUpload.UploadOnRemoteServer(file, fileName);
                                    files.Add(fileModel);

                                    project.Files = files;

                                    string updated = projectManager.Update(project);

                                    if (updated.Equals("1"))
                                    {
                                        return RedirectToAction("ViewAll", "Project");
                                    }
                                    else
                                    {
                                        ViewData["Message"] = updated;
                                        ViewBag.Files = fileManager.GetFileForDropDown(project.Id);
                                        return View(project);
                                    }
                                }
                                else
                                {
                                    // adding new file on existing project
                                    if (file != null)
                                    {
                                        project.State = 1;

                                        Models.File fileModel = new File();

                                        fileModel.ProjectId = project.Id;
                                        fileModel.FileName = file.FileName;
                                        fileModel.FileExtension = file.ContentType;
                                        fileModel.State = 1;
                                        fileModel.FileNameWithExtension =
                                            fileModel.FileName + "." + fileModel.FileExtension;

                                        string fileName = Guid.NewGuid().ToString() + "_" + fileModel.FileName;
                                        fileModel.Url = "http://www.project-manager.somee.com/ProjectFiles/" + fileName;

                                        List<File> files = new List<File>();
                                        fileUpload.UploadOnRemoteServer(file, fileName);
                                        files.Add(fileModel);

                                        project.Files = files;

                                        string updated = projectManager.Update(project);

                                        if (updated.Equals("1"))
                                        {
                                            return RedirectToAction("ViewAll", "Project");
                                        }
                                        else
                                        {
                                            ViewData["Message"] = updated;
                                            ViewBag.Files = fileManager.GetFileForDropDown(project.Id);
                                            return View(project);
                                        }
                                    }
                                    else
                                    {
                                        project.State = 1;
                                        string updated = projectManager.Update(project);

                                        if (updated.Equals("1"))
                                        {
                                            return RedirectToAction("ViewAll", "Project");
                                        }
                                        else
                                        {
                                            ViewData["Message"] = updated;
                                            ViewBag.Files = fileManager.GetFileForDropDown(project.Id);
                                            return View(project);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Invalid Start and End Date");
                                ViewBag.Files = fileManager.GetFileForDropDown(project.Id);
                                return View(project);
                            }
                        }
                        else
                        {
                            ViewData["Message"] = Alert.AlertGenerate("Failed", "Failed", "Fill up all fields correctly");
                            ViewBag.Files = fileManager.GetFileForDropDown(project.Id);
                            return View(project);
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

        // view details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (projectManager.IsProjectExists(id))
                {
                    Project project = projectManager.GetById(id);
                    ViewBag.Files = fileManager.GetFile(id);
                    taskManager.MakeSeenTask(id, user.Id);
                    List<TaskViewModel> viewModels = taskManager.GetTasksByProjectId(id);
                    ViewBag.Tasks = viewModels;
                    ViewBag.TaskCount = viewModels.Count;
                    ViewData["ProjectId"] = project.Id;

                    return View(project);
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

        // download files
        public ActionResult Download(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                
                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
                    Models.File file = fileManager.GetFileById(id);
                    WebClient client = new WebClient();
                    var buffer = client.DownloadData(file.Url);
                    return File(buffer, file.FileExtension, file.FileName);
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

        // remove project
        public ActionResult Remove(int id)
        {
            var authData = HttpContext.Session.GetString("userInfo");

            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);

                if (userAccess.HasAccess(user.Id, 1, user.DesignationId))
                {
                    if (projectManager.IsProjectExists(id))
                    {
                        Project project = projectManager.GetProjectByProjectIdWithFiles(id);
                        project.State = 0;
                        foreach (File file in project.Files)
                        {
                            file.State = 0;
                        }

                        projectManager.Update(project);
                        return RedirectToAction("ViewAll", "Project");
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
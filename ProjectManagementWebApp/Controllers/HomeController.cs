using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private HomeManager homeManager;

        public HomeController()
        {
            homeManager = new HomeManager();
        }

        public IActionResult Index()
        {
            var authData = HttpContext.Session.GetString("userInfo");
            
            if (authData != "")
            {
                User user = JsonConvert.DeserializeObject<User>(authData);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName",user.Name);

                ViewData["NoOfProjects"] = homeManager.NumberOfProjects();
                ViewData["NoOfTasks"] = homeManager.NumberOfTasks();
                ViewData["NoOfNewTasks"] = homeManager.NewTasks(user.Id);
                ViewData["NoOfNewComments"] = homeManager.NumberOfNewComments(user.Id);

                ViewBag.NewTasks = homeManager.NewTaskList(user.Id);
                ViewBag.NewComments = homeManager.GetNewCommentList(user.Id);

                List<int> projects = homeManager.GetProjectNumberByMonth();

                ViewData["Jan"] = projects[0];
                ViewData["Feb"] = projects[1];
                ViewData["Mar"] = projects[2];
                ViewData["Apr"] = projects[3];
                ViewData["May"] = projects[4];
                ViewData["Jun"] = projects[5];
                ViewData["Jul"] = projects[6];
                ViewData["Aug"] = projects[7];
                ViewData["Sept"] = projects[8];
                ViewData["Oct"] = projects[9];
                ViewData["Nov"] = projects[10];
                ViewData["Dec"] = projects[11];

                List<int> projectCount = homeManager.ThisAndLastTwoYearProjectNumber();
                ViewData["ThisYear"] = projectCount[0];
                ViewData["LastYear"] = projectCount[1];
                ViewData["PrevLastYear"] = projectCount[2];

                return View();
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");
                HttpContext.Session.SetInt32("UserId", -9999);
                HttpContext.Session.SetString("UserName", "");

                return RedirectToAction("Login", "LogIn");
            }
            
        }

        // log out
        public IActionResult Logout()
        {
            var user = HttpContext.Session.GetString("userInfo");

            if (user != "")
            {
                HttpContext.Session.SetString("userInfo","");
                HttpContext.Session.SetInt32("UserId", -9999);
                HttpContext.Session.SetString("UserName", "");

                return RedirectToAction("Login", "LogIn");
            }
            else
            {
                return RedirectToAction("Login", "LogIn");
            }
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult AccessDenied()
        {
            var user = HttpContext.Session.GetString("userInfo");

            if (user != "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LogIn");
            }
            
        }
    }
}

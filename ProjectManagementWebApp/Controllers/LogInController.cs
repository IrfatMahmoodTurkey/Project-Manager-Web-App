using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using ProjectManagementWebApp.Manager;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.ViewModels;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Controllers
{
    public class LogInController : Controller
    {
        private UserAuthenticationManager authManager;

        public LogInController()
        {
            authManager = new UserAuthenticationManager();
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.SetString("userInfo", "");
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = authManager.CheckAuthentication(model);

                if (user != null)
                {
                    var userInfo = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("userInfo", userInfo);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("userInfo", "");

                    ViewData["Message"] = Alert.AlertGenerate("Falied","Log in Falied","Enter Valid Email or Password");
                    return View();
                }
            }
            else
            {
                HttpContext.Session.SetString("userInfo", "");
                ViewData["Message"] = Alert.AlertGenerate("Falied", "Log in Falied", "Fill up all fields correctly");
                return View();
            }
        }
    }
}
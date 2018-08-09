using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private UserViewModel currentUser = null;
        private DataContextViewModel db = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");
        // GET: /<controller>/
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string username, string password)
        {
            if (db.Login(username, password) == 0)
            {
                HttpContext.Session.SetString("username", username);
                return View("Welcome");
            }
            else
            {
                ViewBag.error = "Invalid";
                return View("../Shared/Error");
            }       
        }


        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                db.AddUser(user);
                //currentUser = user;
                ViewBag.user = user;
                return View("../Users/Verify");
                //return RedirectToAction("Home", "Index");
            }
            catch
            {
                ViewBag.error = "Invalid";
                return View("../Shared/Error");
            }
        }

        [Route("verify")]
        public IActionResult Verify(string code, string mail)
        {
            if (code.Equals("12345"))
            {                
                db.setVerified(mail);
                return View("Welcome");
            } else {
                return View("Bad code");
            }
        }

    }
}


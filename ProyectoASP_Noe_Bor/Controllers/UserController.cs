using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Models;
using OfficeOpenXml.Core.ExcelPackage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        
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
                string userID = db.getUserId(username).ToString();
                HttpContext.Session.SetString("userID", userID);
                //string username = HttpContext.Current.User.Identity.Name;
                return RedirectToAction("Index", "Home");
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

        [HttpGet]
        [Route("generateExcel")]
        public ActionResult GenerateExcel()
        {
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipt");
                //First add the headers
                
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Zona";
                worksheet.Cell(1, 4).Value = "Beneficios (euros)";
                

                package.Save(); //Save the workbook.
            }
            ViewBag.URL = URL;
            return Redirect(URL);
        }

    }
}


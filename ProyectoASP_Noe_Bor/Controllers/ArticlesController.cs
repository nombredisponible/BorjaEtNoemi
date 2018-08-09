using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Models;

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("articles")]
    public class ArticlesController : Controller
    {

        private DataContextViewModel db = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");
        // GET: Articles
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Articles/Details/5
        [Route("details")]
        public ActionResult Details(int id)
        {
            return View(db.GetProduct(id));
        }

        // GET: Articles/Create
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public IActionResult Create(ArticleViewModel art, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return Content("File not selected");
            }
            else
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);
                art.Imagen = photo.FileName;
            }
            try
            {
                db.Insert(art);

                return RedirectToAction("Articles", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Articles/Edit/5
        [Route("edit")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Articles/Delete/5
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Articles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
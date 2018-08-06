using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("articles")]
    public class ArticlesController : Controller
    {
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
            return View();
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
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
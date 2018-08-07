using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Models;

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        
        private DataContextViewModel db = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("articles")]
        public IActionResult Articles()
        {
            ViewData["Message"] = "Your articles page.";

            //List<ArticleViewModel> articulosPrueba = new List<ArticleViewModel>()
            //{
            //    new ArticleViewModel(1,"art01",4,4.80,"foto01"),
            //    new ArticleViewModel(2,"art02",4,4.80,"foto02"),
            //    new ArticleViewModel(3,"art03",4,4.80,"foto03"),
            //    new ArticleViewModel(4,"art04",4,4.80,"foto04"),
            //    new ArticleViewModel(5,"art05",4,4.80,"foto05"),
            //    new ArticleViewModel(6,"art06",4,4.80,"foto06"),
            //    new ArticleViewModel(7,"art07",4,4.80,"foto07"),
            //    new ArticleViewModel(8,"art08",4,4.80,"foto08"),
            //    new ArticleViewModel(9,"art09",4,4.80,"foto09")
            //};

            return View(db.GetAllProducts());
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

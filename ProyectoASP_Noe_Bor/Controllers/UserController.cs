using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {

        private DataContextViewModel db;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UserController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            db = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");
        }
        

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
        [Route("pdf")]
        public IActionResult Pdf()
        {
            byte[] userId;
            int id = 0;
            if (HttpContext.Session.TryGetValue("userID", out userId))
            {
                Int32.TryParse(System.Text.Encoding.UTF8.GetString(userId), out id);
            }
            List<BillViewModel> listaFacturas = db.getFacturasPDF(id);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.pdf";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }


            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(sWebRootFolder + "/" + sFileName, FileMode.Create));



            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Detalles de su facturación"));
            doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá el nombre, apellido y zona
            // de nuestros visitante.
            foreach(BillViewModel factura in listaFacturas) { 

            PdfPTable tblPrueba = new PdfPTable(2);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell idFactura = new PdfPCell(new Phrase("Id Factura", _standardFont));
            idFactura.BorderWidth = 0;
            idFactura.BorderWidthBottom = 0.75f;

            PdfPCell fecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            fecha.BorderWidth = 0;
            fecha.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(idFactura);
            tblPrueba.AddCell(fecha);

                

            // Cambiamos el tipo de Font para el listado
            _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);

            // Llenamos la tabla con el primer resultado
            idFactura = new PdfPCell(new Phrase("Luis", _standardFont));
            idFactura.BorderWidth = 0;
            fecha = new PdfPCell(new Phrase("López", _standardFont));
            fecha.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(idFactura);
            tblPrueba.AddCell(fecha);

            // Finalmente, añadimos la tabla al documento PDF
            doc.Add(tblPrueba);

                foreach (BillDetailViewModel linea in factura.LineasFactura) {
                    PdfPTable tblPrueba2 = new PdfPTable(3);
                    tblPrueba2.WidthPercentage = 100;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell nomProducto = new PdfPCell(new Phrase("Producto", _standardFont));
                    nomProducto.BorderWidth = 0;
                    nomProducto.BorderWidthBottom = 0.75f;

                    PdfPCell cantidad = new PdfPCell(new Phrase("Cantidad", _standardFont));
                    cantidad.BorderWidth = 0;
                    cantidad.BorderWidthBottom = 0.75f;

                    PdfPCell subtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                    subtotal.BorderWidth = 0;
                    subtotal.BorderWidthBottom = 0.75f;

                    // Añadimos las celdas a la tabla
                    tblPrueba2.AddCell(nomProducto);
                    tblPrueba2.AddCell(cantidad);
                    tblPrueba2.AddCell(subtotal);



                    // Cambiamos el tipo de Font para el listado
                    _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);

                    // Llenamos la tabla con el primer resultado
                    nomProducto = new PdfPCell(new Phrase("Luis", _standardFont));
                    nomProducto.BorderWidth = 0;
                    cantidad = new PdfPCell(new Phrase("López", _standardFont));
                    cantidad.BorderWidth = 0;
                    subtotal = new PdfPCell(new Phrase("López", _standardFont));
                    subtotal.BorderWidth = 0;

                    // Añadimos las celdas a la tabla
                    tblPrueba2.AddCell(nomProducto);
                    tblPrueba2.AddCell(cantidad);
                    tblPrueba2.AddCell(subtotal);

                    // Finalmente, añadimos la tabla al documento PDF
                    doc.Add(tblPrueba2);

                }//foreach 2

            } //foreach 1

            doc.Close();
            writer.Close();

            ViewBag.URL = URL;
            return View("../Users/Bajar");

        }

    }
}


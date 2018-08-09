using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoASP_Noe_Bor.Helpers;
using ProyectoASP_Noe_Bor.Models;

namespace ProyectoASP_Noe_Bor.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private DataContextViewModel db = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");

        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.LineasFactura.Sum(item => item.Producto.Precio * item.Cantidad);
            ViewBag.cartItems = cart.LineasFactura.Count();
            ViewData["resumenCarrito"] = string.Empty;
            foreach (BillDetailViewModel item in cart.LineasFactura)
            {
                ViewData["resumenCarrito"] += string.Format("{1} {0}<br>", item.Producto.Nombre, item.Cantidad);
            }
            ViewData["itemsCarrito"] = cart.LineasFactura.Count.ToString();
            return View("../home/ShoppingCart", cart);
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            DataContextViewModel productModel = new DataContextViewModel("server=localhost;port=3306;database=proyectoasp_noe_bor;user=admin;password=1111");
            if (SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart") == null)
            {
                BillViewModel cart = new BillViewModel();
                cart.LineasFactura.Add(new BillDetailViewModel() { Producto = productModel.GetProduct(int.Parse(id)), Cantidad = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                BillViewModel cart = SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart.LineasFactura[index].Cantidad++;
                }
                else
                {
                    cart.LineasFactura.Add(new BillDetailViewModel() { Producto = productModel.GetProduct(int.Parse(id)), Cantidad = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            BillViewModel cart = SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.LineasFactura.RemoveAt(cart.LineasFactura.Where(f => f.Producto.Id == index).FirstOrDefault().Id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("checkout")]
        public IActionResult Checkout()
        {
            int userID = 1; //TODO usuario loggeado 

            BillViewModel cart = SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart");
            if (cart == null || !(cart.LineasFactura.Count > 0))
            {

            }
            else
            {
                db.BuyCart(userID, cart);
            }
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            BillViewModel cart = SessionHelper.GetObjectFromJson<BillViewModel>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.LineasFactura.Count; i++)
            {
                if (cart.LineasFactura[i].Producto.Id.ToString().Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class BillDetailViewModel
    {
        public BillDetailViewModel()
        {
        }

        public BillDetailViewModel(int cantidad, ArticleViewModel producto)
        {
            Cantidad = cantidad;
            Producto = producto;
            Subtotal = producto.Precio * cantidad;
        }

        public int Id { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
        public ArticleViewModel Producto { get; set; }
    }
}

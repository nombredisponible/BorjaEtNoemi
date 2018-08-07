using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class BillViewModel
    {
        public BillViewModel(DateTime fecha, UserViewModel cliente, List<BillDetailViewModel> lineasFactura)
        {
            Fecha = fecha;
            Cliente = cliente;
            LineasFactura = lineasFactura;
        }

        public int Id { get; }
        public DateTime Fecha { get; set; }
        public UserViewModel Cliente { get; set; }
        public List<BillDetailViewModel> LineasFactura { get; set; }
    }
}

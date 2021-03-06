﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class BillViewModel
    {
        public BillViewModel()
        {
            this.Cliente = new UserViewModel();
            this.LineasFactura = new List<BillDetailViewModel>();
            this.Fecha = DateTime.Now;
        }

        public BillViewModel(DateTime fecha, UserViewModel cliente, List<BillDetailViewModel> lineasFactura)
        {
            Fecha = fecha;
            Cliente = cliente;
            LineasFactura = lineasFactura;
        }

        public BillViewModel(DateTime fecha, List<BillDetailViewModel> lineasFactura)
        {
            Fecha = fecha;            
            LineasFactura = lineasFactura;
        }

        public int Id { get; }
        public int IdBD { get; set; }
        public DateTime Fecha { get; set; }
        public UserViewModel Cliente { get; set; }
        public List<BillDetailViewModel> LineasFactura { get; set; }
    }
}

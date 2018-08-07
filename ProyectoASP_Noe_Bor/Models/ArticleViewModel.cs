using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
        }

        public ArticleViewModel(int id, string nombre, int cantidad, double precio, string imagen)
        {
            Id = id;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
            Imagen = String.IsNullOrWhiteSpace(imagen) ? "":imagen;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Imagen { get; set; }
    }
}

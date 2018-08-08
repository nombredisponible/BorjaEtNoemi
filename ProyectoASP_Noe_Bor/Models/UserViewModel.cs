using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(string nickname, string nombre, string apellidos, string nIF, string mail, string contrasena)
        {
            Nickname = nickname;
            Nombre = nombre;
            Apellidos = apellidos;
            NIF = nIF;
            Mail = mail;
            Contrasena = contrasena;
        }

        public int Id { get; }
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NIF { get; set; }
        public string Mail { get; set; }
        public string Contrasena { get; set; }
        public bool Admin { get; set; }
    }
}

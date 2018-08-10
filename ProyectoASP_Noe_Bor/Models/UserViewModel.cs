using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }
        public UserViewModel(int id, string nickname, string nombre, string apellidos, string nIF, string mail, string contrasena, bool admin, int validado)
        {
            Id = id;
            Nickname = nickname;
            Nombre = nombre;
            Apellidos = apellidos;
            NIF = nIF;
            Mail = mail;
            Contrasena = contrasena;
            Admin = admin;
            Validado = validado;
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NIF { get; set; }
        public string Mail { get; set; }
        public string Contrasena { get; set; }
        public bool Admin { get; set; }
        public int Validado { get; }
    }

   

}

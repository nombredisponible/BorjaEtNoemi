using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProyectoASP_Noe_Bor.Models
{
    public class DataContextViewModel
    {
        public string ConnectionString { get; set; }

        public DataContextViewModel(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<ArticleViewModel> GetAllProducts()
        {
            List<ArticleViewModel> list = new List<ArticleViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM producto", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        int Id = reader.GetInt32("id");
                        string Nombre = reader.GetString("nombre");
                        double Precio = reader.GetDouble("precio");
                        string Imagen = reader.GetString("imagen");
                        int Cantidad = reader.GetInt32("cantidad");

                        list.Add(new ArticleViewModel(Id, Nombre, Cantidad, Precio, Imagen));

                    }
                }

            }
            return list;
        }

        public ArticleViewModel GetProduct(int id)
        {
            ArticleViewModel arti = new ArticleViewModel();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM producto where id = " + id, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        int Id = reader.GetInt32("id");
                        string Nombre = reader.GetString("nombre");
                        double Precio = reader.GetDouble("precio");
                        string Imagen = reader.GetString("imagen");
                        int Cantidad = reader.GetInt32("cantidad");

                        arti = new ArticleViewModel(Id, Nombre, Cantidad, Precio, Imagen);

                    }
                }

            }
            return arti;
        }

        public void Insert(ArticleViewModel article)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(String.Format("INSERT INTO producto (`Nombre`, `Precio`, `Imagen`, `Cantidad`) VALUES('{0}','{1}','{2}','{3}');", article.Nombre, article.Precio, article.Imagen, article.Cantidad), conn);
                int filas = cmd.ExecuteNonQuery();
                Console.WriteLine(filas.ToString());
            }
        }


        public void Delete(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM producto WHERE id = " + id, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddUser(UserViewModel user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(String.Format("INSERT INTO usuario (`Nickname`, `Apellidos`, `NIF`, `Mail`, `Nombre`, `Admin`, `Contrasena`,`Verificado`) VALUES('{0}','{1}','{2}','{3}', '{4}', '{5}', '{6}', '{7}');", user.Nickname, user.Apellidos, user.NIF, user.Mail, user.Nombre, 1, user.Contrasena, 0), conn);
                int filas = cmd.ExecuteNonQuery();
                Console.WriteLine(filas.ToString());
            }
                //Validar a traves de email
                // servidor SMTP

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("hiberusclaseaspcoremvc@gmail.com", "111??aaa");
                client.EnableSsl = true;

                // 
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("hiberusclaseaspcoremvc@gmail.com");
                mailMessage.To.Add(user.Mail);
                mailMessage.Body = "Introduzca el código 12345 para terminar su proceso de inscripción";
                mailMessage.Subject = "Borja & Noemi eShop: verifique su cuenta";

                string output = "enviado";
                try
                {
                    client.Send(mailMessage);
                }
                catch (Exception e) { output = e.ToString() + "no enviado"; }                
            
        }
        
        public void BuyCart(int userId,BillViewModel cart)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(String.Format("INSERT INTO factura (`Fecha`, `IdCliente`) VALUES('{0}','{1}');", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),userId), conn);
                int filas = cmd.ExecuteNonQuery();
                foreach (BillDetailViewModel item in cart.LineasFactura)
                {
                    MySqlCommand cmd1 = new MySqlCommand(String.Format("INSERT INTO lineafactura (`IdFactura`, `IdProducto`, `Cantidad`, `Subtotal`) VALUES((SELECT max(id) from factura),'{0}','{1}','{2}');", item.Producto.Id, item.Cantidad, item.Subtotal.ToString().Replace(",",".")), conn);
                    filas = cmd1.ExecuteNonQuery();
                }
                Console.WriteLine(filas.ToString());
            }
        }

        public int Login(string nickname, string contrasena)
        {
            using (MySqlConnection conn = GetConnection())
            {
                UserViewModel user = new UserViewModel();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario where Nickname = '" + nickname + "'",conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        int id = reader.GetInt32("id");
                        string Unickname = reader.GetString("nickname");
                        string apellidos = reader.GetString("apellidos");
                        string nif = reader.GetString("nif");
                        string mail = reader.GetString("mail");
                        string nombre = reader.GetString("nombre");
                        bool admin = reader.GetBoolean("admin");
                        string Ucontrasena = reader.GetString("contrasena");
                        int codVal = reader.GetInt32("codValidacion");
                        int isVal = reader.GetInt32("verificado");

                        user = new UserViewModel(id, Unickname, nombre, apellidos, nif, mail, Ucontrasena, admin, isVal);
                        
                    }
                    if(contrasena == user.Contrasena)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public int getUserId(string name)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario where nickname = '" + name + "'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        id = reader.GetInt32("id");

                    }
                }
            }
            return id;
        }

        public UserViewModel getUserById(int id)
        {
            UserViewModel ret = new UserViewModel();
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario where id = '" + id + "'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        ret.Id = reader.GetInt32("id");
                        ret.Nickname = reader.GetString("Nickname");
                        ret.Apellidos = reader.GetString("Apellidos");
                        ret.NIF = reader.GetString("NIF");
                        ret.Mail = reader.GetString("Mail");
                        ret.Nombre = reader.GetString("Nombre");
                        ret.Admin = reader.GetBoolean("Admin");
                        ret.Contrasena = reader.GetString("Contrasena");

                    }
                }
            }
            return ret;
        }

        public void setVerified(string mail)
        {
            
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET Verificado = 1 WHERE usuario.Mail = '" + mail + "'", conn);
                int filas = cmd.ExecuteNonQuery();
                Console.WriteLine(filas.ToString());
            }
        }
    }
}
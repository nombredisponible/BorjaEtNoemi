using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
                MySqlCommand cmd = new MySqlCommand(String.Format("INSERT INTO producto (`Nombre`, `Precio`, `Imagen`, `Cantidad`) VALUES('{0}','{1}','{2}','{3}');", article.Nombre,article.Precio,article.Imagen,article.Cantidad), conn);
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

    }
}
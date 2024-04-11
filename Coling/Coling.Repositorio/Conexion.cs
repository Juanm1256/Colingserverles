using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Coling.Shared;

namespace DAL
{
    public class conexion
    {

        public static string CONECTAR
        {
            get { return @"Data Source=(local); Initial Catalog=ColingSecurityBD; Integrated Security=True; TrustServerCertificate=true;"; }
            //get { return ConfigurationManager.ConnectionStrings["cadena"].ToString(); }
        }
        public static DataSet EjecutarDataSet(string consulta)
        {
            string p = conexion.CONECTAR;
            SqlConnection conectar = new SqlConnection(conexion.CONECTAR);
            conectar.Open();
            SqlCommand cmd = new SqlCommand(consulta, conectar);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds, "TABLA");
            return ds;
        }

        public static void Ejecutar(string consulta)
        {
            SqlConnection conectar = new SqlConnection(conexion.CONECTAR);
            conectar.Open();
            SqlCommand cmd = new SqlCommand(consulta, conectar);
            cmd.CommandTimeout = 5000;
            cmd.ExecuteNonQuery();
        }

        public static int EjecutarEscalar(string consulta)
        {
            SqlConnection conectar = new SqlConnection(conexion.CONECTAR);
            conectar.Open();

            SqlCommand cmd = new SqlCommand(consulta, conectar);
            cmd.CommandTimeout = 5000;
            int dev = Convert.ToInt32(cmd.ExecuteScalar());
            return dev;
        }
        public static DataTable EjecutarDataTabla(string consulta, string tabla)
        {
            string p = conexion.CONECTAR;
            SqlConnection conectar = new SqlConnection(conexion.CONECTAR);
            SqlCommand cmd = new SqlCommand(consulta, conectar);
            cmd.CommandTimeout = 5000;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable(tabla);
            da.Fill(dt);
            return dt;
        }

        public static bool Insertardatas(string consulta, string id, string user, string pass, string rol, string estado)
        {
            bool sw = false;
            using (SqlConnection conectar = new SqlConnection(conexion.CONECTAR))
            {
                conectar.Open();
                using (SqlCommand command = new SqlCommand(consulta, conectar))
                {
                    // Agrega parámetros a la consulta
                    command.Parameters.AddWithValue("@Idusuario", id);
                    command.Parameters.AddWithValue("@usuario", user);
                    command.Parameters.AddWithValue("@password", pass);
                    command.Parameters.AddWithValue("@rol", rol);
                    command.Parameters.AddWithValue("@estado", estado);

                    // Ejecuta la consulta y obtén el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        sw = true;
                    }
                }
            }
            return sw;
        }



        public static RegistrarUsuario Obtenerdata(string consulta, string id)
        {
            RegistrarUsuario usuario = null;
            using (SqlConnection conectar = new SqlConnection(conexion.CONECTAR))
            {
                conectar.Open();
                using (SqlCommand command = new SqlCommand(consulta, conectar))
                {
                    // Agrega parámetros a la consulta
                    command.Parameters.AddWithValue("@Idusuario", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.Read())
                        {
                            // Crea un objeto RegistrarUsuario y establece sus propiedades
                            usuario = new RegistrarUsuario
                            {
                                Id = reader["idusuario"].ToString(),
                                UserName = reader["nombreuser"].ToString(),
                                Password = reader["password"].ToString(),
                                Rol = reader["rol"].ToString(),
                                Estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public static DataTable Insertar(string consulta, string tabla)
        {
            string p = conexion.CONECTAR;
            SqlConnection conectar = new SqlConnection(conexion.CONECTAR);
            SqlCommand cmd = new SqlCommand(consulta, conectar);
            cmd.CommandTimeout = 5000;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable(tabla);
            da.Fill(dt);
            return dt;
        }
    }
}
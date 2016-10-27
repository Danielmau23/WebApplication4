using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace WebApplication4.Models
{
    public class BaseDatos
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;
        string consulta;
        string credenciales = "Data Source=MAURICIO;Initial Catalog=pruebaDotcreek;Integrated Security=True";

        //***************************************************************************************
        //*************************** plantillas ********************************************
        //***************************************************************************************

        public List<plantillas> plantillasList(int usuario)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("select p.idPlantilla , p.mensaje , p.html  from plantillas as p , planUsu as u where p.idPlantilla = u.plantilla and u.usuario = {0}", usuario);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                plantillas nodo;
                List<plantillas> objetos = new List<plantillas>();

                while (lector.Read() == true)
                {
                    nodo = new plantillas();

                    nodo.idPlantilla = Convert.ToInt32(lector[0]);
                    if (lector[1].GetType().Name != "DBNull")
                        nodo.mensaje = Convert.ToString(lector[1]);
                    if (lector[2].GetType().Name != "DBNull")
                        nodo.html = Convert.ToString(lector[2]);

                    objetos.Add(nodo);
                }
                lector.Close();
                conexion.Close();
                return objetos;

            }
            catch (Exception e)
            {
                return null;
            }
        }


        //***************************************************************************************
        //****************************** ID Usuario *************************************
        //***************************************************************************************

        public int idUsuario(string nom, string pas)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("select idUsuario from usuarios where nombre = '{0}' and contraseña = '{1}'", nom, pas);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();

                int num = -10;
                if (lector.Read() == true)
                {
                    num = Convert.ToInt32(lector[0]);
                    lector.Close();
                    conexion.Close();
                    return num;
                }
                else
                {
                    lector.Close();
                    conexion.Close();
                    return num;
                }
            }
            catch (Exception e)
            {
                return -10;
            }
        }



        //***************************************************************************************
        //****************************** Nueva  Plantilla **************************************
        //***************************************************************************************

        public bool nuevaPlantilla(string mensaje, int idUsuario)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("insert into plantillas (mensaje , html) values ('{0}','Si') ", mensaje);
                comando = new SqlCommand(consulta, conexion);
                int num = comando.ExecuteNonQuery();
                if (num != 0)
                {
                    consulta = string.Format("SELECT TOP 1 idPlantilla FROM plantillas order by idPlantilla DESC");
                    comando = new SqlCommand(consulta, conexion);
                    lector = comando.ExecuteReader();
                    if (lector.Read() == true)
                    {
                        int idPlan = Convert.ToInt32(lector[0]);
                        consulta = string.Format("insert into planUsu (usuario , plantilla) values ({0},{1}) ", idUsuario, idPlan);
                        comando = new SqlCommand(consulta, conexion);
                        lector.Close();
                        num = comando.ExecuteNonQuery();
                        if (num != 0)
                        {
                            lector.Close();
                            conexion.Close();
                            return true;
                        }

                        lector.Close();
                        conexion.Close();
                        return false;
                        
                    }
                    lector.Close();
                    conexion.Close();
                    return false;
                }
                else
                {
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using GoEmail;

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
        //****************************** Login **************************************
        //***************************************************************************************

        public bool login(string nom, string pas, string cor)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("select * from usuarios where nombre = '{0}' and contraseña = '{1}' and correo = '{2}'", nom, pas,cor);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();

                if (lector.Read() == true)
                {
                    lector.Close();
                    conexion.Close();
                    return true;
                }
                else
                {
                    lector.Close();
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //***************************************************************************************
        //****************************** Register **************************************
        //***************************************************************************************

        public bool register(string nom,string cor, string pas )
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("insert into usuarios (nombre,correo,contraseña) values ('{0}','{1}','{2}')", nom, cor, pas);
                comando = new SqlCommand(consulta, conexion);
                int num = comando.ExecuteNonQuery();

                if (num != 0)
                {
                    conexion.Close();
                    return true;
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

        //***************************************************************************************
        //****************************** New Password **************************************
        //***************************************************************************************

        public bool newPass(string nom, string pas)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("UPDATE usuarios SET contraseña = '{0}' WHERE nombre = '{1}'", pas, nom);
                comando = new SqlCommand(consulta, conexion);
                int num = comando.ExecuteNonQuery();
                if (num != 0)
                {
                    conexion.Close();
                    return true;
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
        //****************************** Usuario *************************************
        //***************************************************************************************

        public usuario usuario(int id)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("select * from usuarios where idUsuario = '{0}'",id);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                usuario nodo;
                if (lector.Read() == true)
                {
                    nodo= new usuario(Convert.ToString(lector[1]),Convert.ToString(lector[2]),Convert.ToString(lector[3]));
                    lector.Close();
                    conexion.Close();
                    return nodo;
                }
                else
                {
                    lector.Close();
                    conexion.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
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


        public plantillas plantillasGetEdit(int id)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("select * from plantillas where idPlantilla = {0}", id);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                plantillas nodo;
                while (lector.Read() == true)
                {
                    nodo = new plantillas();
                    nodo.idPlantilla = Convert.ToInt32(lector[0]);
                    if (lector[1].GetType().Name != "DBNull")
                        nodo.mensaje = Convert.ToString(lector[1]);
                    if (lector[2].GetType().Name != "DBNull")
                        nodo.html = Convert.ToString(lector[2]);
                    
                    lector.Close();
                    conexion.Close();
                    return nodo;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool plantillasSetEdit(plantillas sudo)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                string query = "UPDATE plantillas SET mensaje= @mensaje WHERE idPlantilla = @idPlantilla";
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                sqlCommand.Parameters.Add("@idPlantilla", System.Data.SqlDbType.Int).Value = sudo.idPlantilla;

                if (sudo.mensaje.ToString() == "")
                    sqlCommand.Parameters.Add("@mensaje", System.Data.SqlDbType.Text).Value = System.Data.SqlTypes.SqlDateTime.Null;
                else
                    sqlCommand.Parameters.Add("@mensaje", System.Data.SqlDbType.Text).Value = sudo.mensaje;
                

                conexion.Open();

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    conexion.Close();
                    return true;
                }
                catch (Exception exc)
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


        public bool eliminarPlantilla(int id)
        {
            try
            {
                conexion = new SqlConnection(credenciales);
                conexion.Open();
                consulta = string.Format("DELETE FROM planUsu WHERE plantilla = {0} ", id);
                comando = new SqlCommand(consulta, conexion);
                int num = comando.ExecuteNonQuery();
                if (num != 0)
                {
                    consulta = string.Format("DELETE FROM plantillas WHERE idPlantilla = {0} ", id);
                    comando = new SqlCommand(consulta, conexion);
                    num = comando.ExecuteNonQuery();
                    if (num != 0)
                    {
                        conexion.Close();
                        return true;
                    }

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

        //***************************************************************************************
        //*************************** Enviar Correo  ********************************************
        //***************************************************************************************

        public bool enviar(correo email)
        {
            GoEmailv2 mensaje = new GoEmailv2();

            if (mensaje.Enviar(email.para, email.asunto, email.mensaje, email.nombre, email.correo2, email.contrasena, true))
                return true;
            else
            {
                string mnsError = GoEmailv2.error;
                return false;
            }
        }


    }
}
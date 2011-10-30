﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Comun;

namespace Acceso
{
    public class clsGestorBD
    {
        private static string strRuta = AppDomain.CurrentDomain.BaseDirectory;
        private static TextReader tr = new StreamReader(strRuta + "\\Acceso.txt");
        private static string strServidor = tr.ReadLine();
        private static string strCatalogo = tr.ReadLine();
        private static string strUsuario = tr.ReadLine();
        private static string strContrasena = tr.ReadLine();

        public static SqlConnection conectar()
        {
            SqlConnection sqlConexion = new SqlConnection("Data Source=" + strServidor + "; Initial Catalog=" + strCatalogo + ";Integrated Security=SSPI");

            return sqlConexion;
        }

        public static int ejecutarStoredProcedure(string strNombreStoredProcedure, List<SqlParameter> lstParametrosSQL)
        {
            SqlConnection sqlConexion = conectar();

            try
            {
                sqlConexion.Open();

                SqlCommand sqlComando = new SqlCommand(strNombreStoredProcedure, sqlConexion);
                sqlComando.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < lstParametrosSQL.Count; i++)
                {
                    sqlComando.Parameters.Add(lstParametrosSQL[i]);
                }

                SqlDataAdapter sqlAdaptador = new SqlDataAdapter(sqlComando);
                DataTable dtTabla = new DataTable();
                sqlAdaptador.Fill(dtTabla);

                sqlConexion.Close();

                return (int)(lstParametrosSQL[lstParametrosSQL.Count - 1].Value);
            }
            catch (Exception ex)
            {
                clsComun.registrarErrorLog(ex.ToString());
                return 0;
            }
            finally
            {
                if (sqlConexion != null)
                {
                    sqlConexion.Close();
                }
            }
        }

        public static DataTable ejecutarStoredProcedure2(string strNombreStoredProcedure, List<SqlParameter> lstParametrosSQL)
        {
            SqlConnection sqlConexion = conectar();

            try
            {
                sqlConexion.Open();

                SqlCommand sqlComando = new SqlCommand(strNombreStoredProcedure, sqlConexion);
                sqlComando.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < lstParametrosSQL.Count; i++)
                {
                    sqlComando.Parameters.Add(lstParametrosSQL[i]);
                }

                SqlDataAdapter sqlAdaptador = new SqlDataAdapter(sqlComando);
                DataTable dtTabla = new DataTable();
                sqlAdaptador.Fill(dtTabla);
                
                sqlConexion.Close();

                return dtTabla;
            }
            catch(Exception ex)
            {
                clsComun.registrarErrorLog(ex.ToString());
                return null;
            }
            finally
            {
                if (sqlConexion != null)
                {
                    sqlConexion.Close();
                }
            }
        }
    }
}
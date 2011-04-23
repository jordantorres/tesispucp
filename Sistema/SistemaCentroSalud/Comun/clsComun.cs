﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Modelo;
using System.IO;

namespace Comun
{
    public class clsComun
    {
        public static void registrarErrorLog(string strError)
        {
            try
            {
                string strDia = DateTime.Now.ToString("dd/MM/yyyy");
                string strHora = DateTime.Now.ToString("hh:mm:ss");
                string strRuta = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
                string strLinea = strDia + " - " + strHora + " >>>> " + strError;

                StreamWriter sw = File.AppendText(strRuta);

                sw.WriteLine(strLinea);

                sw.Close();
            }
            catch (Exception)
            {
            }
        }

        public static bool validarCorreoElectronico(string strEmail)
        {
            string strExpresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(strEmail, strExpresion))
            {
                if (Regex.Replace(strEmail, strExpresion, String.Empty).Length == 0)
                { 
                    return true; 
                }
                else
                { 
                    return false; 
                }
            }
            else
            { 
                return false; 
            }
        }

        public static bool enviarCorreo(string destinatario, clsPersonal objPersonal)
        {
            try
            {
                MailMessage correo = new MailMessage();

                string asunto = "Contraseña Biblioteca Virtual";
                string cuerpo = "";

                correo.From = new MailAddress("wilder.young@gmail.com", "Biblioteca Virtual Soporte", System.Text.Encoding.UTF8);
                correo.To.Add(destinatario);
                correo.Subject = asunto;
                correo.SubjectEncoding = System.Text.Encoding.UTF8;

                cuerpo += "Estimado(a):\n";
                cuerpo += objPersonal.StrApellidoPaterno + " " + objPersonal.StrApellidoMaterno + ", " + objPersonal.StrNombres + "\n\n";

                cuerpo += "Sus datos de acceso al Sistema son:\n";
                cuerpo += "=====================================\n";
                cuerpo += "Usuario: " + objPersonal.StrUsuario + "\n";
                cuerpo += "Contraseña: " + objPersonal.StrContrasena + "\n\n";
                cuerpo += "*Recuerde que puede cambiar su contraseña\n";
                cuerpo += "\n";


                correo.Body = cuerpo;
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                correo.IsBodyHtml = false;
                correo.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("wilder.young@gmail.com", "97407212");
                try
                {
                    smtp.Send(correo);
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    registrarErrorLog(ex.ToString());
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                registrarErrorLog(ex.ToString());
                return false;
            }
        }

        public static string generarContrasenaAleatoria(int numLongitudContrasena)
        {
            string strCaracteresPermitidos = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            Byte[] bytAleatorio = new Byte[numLongitudContrasena];
            char[] chrCaracteres = new char[numLongitudContrasena];
            int numContadorCaracteresPermitidos = strCaracteresPermitidos.Length;

            for (int i = 0; i < numLongitudContrasena; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(bytAleatorio);
                chrCaracteres[i] = strCaracteresPermitidos[(int)bytAleatorio[i] % numContadorCaracteresPermitidos];
            }

            return new string(chrCaracteres);
        }
    }
}

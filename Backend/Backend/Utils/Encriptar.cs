using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Utils
{
    public static class Encriptar
    {
        /// <summary>
        /// Metodo para encriptacion de password de usuarios registrados
        /// 
        /// </summary>
        /// <param name="input">Contraseña</param>
        /// <returns>Contraseña encriptada</returns>
        public static string  EncriptarPassword(string input)
        {
            MD5 md5Hash = MD5.Create();
            //Convierte el input string a un arreglo de bytes and compute the hasf
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace ApiNetTransportes.Helpers
{
    public class HelperCryptography
    {
        public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha = SHA512.Create();
            //Convertimos contedio a Bytes[]
            byte[] salida = Encoding.UTF8.GetBytes(contenido);
            //creamos las iteraciones
            for (int i = 1; i <= 114; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNetTransportes.Models
{
    public class UsuarioModel
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }
       
        public string Password { get; set; }
       
        public int Telefono { get; set; }
       

    }
}

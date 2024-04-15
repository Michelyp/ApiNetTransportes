using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("v_all_usuarios")]
    public class UsuarioVista
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("CORREO")]
        public string Correo { get; set; }
        [Column("TELEFONO")]
        public int Telefono { get; set; }
        [Column("ROL")]
        public string Rol { get; set; }
        [Column("ESTADO")]
        public string EstadoUsuario { get; set; }

    }
}

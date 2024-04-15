using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("USUARIOS")]
    public class Usuario
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
        [Column("SALT")]
        public string Salt { get; set; }
        [Column("PASS")]
        public byte[] Password { get; set; }
        [Column("TELEFONO")]
        public int Telefono { get; set; }
        [Column("IDROL")]
        public int IdRol { get; set; }
        [Column("IDFACTURACION")]
        public int? IdFacturacion { get; set; }
        [Column("ESTADO")]
        public int? EstadoUsuario { get; set; }

    }
}

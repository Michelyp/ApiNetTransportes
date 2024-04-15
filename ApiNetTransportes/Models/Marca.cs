using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("MARCA")]
    public class Marca
    {
        [Key]
        [Column("IDMARCA")]
        public int IdMarca { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}

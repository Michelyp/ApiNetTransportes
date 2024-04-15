using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("TIPOMOVILIDAD")]

    public class TipoMovilidad
    {

        [Key]
        [Column("IDTIPO")]
        public int IdTipo { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("RESERVA")]
    public class Reserva
    {
        [Key]
        [Column("IDRESERVA")]
        public int IdReserva { get; set; }
        [Column("LUGAR")]
        public string Lugar { get; set; }
        [Column("CONDUCTOR")]
        public string Conductor { get; set; }
        [Column("HORAINICIAL")]
        public TimeSpan? HoraInicial { get; set; }
        [Column("FECHARECOGIDA")]
        public DateTime? FechaRecogida { get; set; }
        [Column("FECHADEVOLUCION")]
        public DateTime? FechaDevolucion { get; set; }
        [Column("HORAFINAL")]
        public TimeSpan? HoraFinal { get; set; }
        [Column("IDCOCHE")]
        public int IdCoche { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("IDESTADORESERVA")]
        public int IdEstadpReserva { get; set; }
    }
}

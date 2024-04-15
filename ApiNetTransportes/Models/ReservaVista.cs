using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
    [Table("V_RESERVAS_LISTA")]
    public class ReservaVista
    {
        [Key]
        [Column("IDRESERVA")]
        public int IdReserva { get; set; }
        [Column("LUGAR")]
        public string Lugar { get; set; }
        [Column("CONDUCTOR")]
        public string Conductor { get; set; }
        [Column("HORAINICIAL")]
        public TimeSpan HoraInicial { get; set; }
        [Column("FECHARECOGIDA")]
        public DateTime FechaRecogida { get; set; }
        [Column("FECHADEVOLUCION")]
        public DateTime FechaDevuelto { get; set; }
        [Column("HORAFINAL")]
        public TimeSpan HoraFinal { get; set; }
        [Column("COCHE")]
        public int Coche { get; set; }
        [Column("NOMBRE")]
        public string NombreUsuario { get; set; }
        [Column("RESERVA")]
        public string ReservaEstado { get; set; }

    }
}

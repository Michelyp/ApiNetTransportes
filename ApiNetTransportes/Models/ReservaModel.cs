using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNetTransportes.Models
{
    public class ReservaModel
    {
        public string Lugar { get; set; }
        public string Conductor { get; set; }
        public TimeSpan HoraInicial { get; set; }
        public DateTime FechaRecogida { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public TimeSpan HoraFinal { get; set; }
        public int IdCoche { get; set; }
    }
}

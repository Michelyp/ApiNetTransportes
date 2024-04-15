using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiNetTransportes.Models
{
        [Table("FACTURACION")]

        public class Facturacion
        {
            [Key]
            [Column("IDFACTURACION")]
            public int IdFacturacion { get; set; }
            [Column("NOMBRE")]
            public string Nombre { get; set; }
            [Column("DIRECCION")]
            public string Direccion { get; set; }
            [Column("CIUDAD")]
            public string Ciudad { get; set; }
            [Column("CODIGOPOSTAL")]
            public int CodigoPostal { get; set; }
            [Column("PAIS")]
            public string Pais { get; set; }
        }
    }

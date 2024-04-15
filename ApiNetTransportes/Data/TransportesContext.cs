using ApiNetTransportes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiNetTransportes.Data
{
    public class TransportesContext:DbContext
    {
        public TransportesContext(DbContextOptions <TransportesContext> options)
            : base(options) { }
        public DbSet<Coche> Coches { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioVista> UsuarioVistas { get; set; }
        public DbSet<CocheVista> CocheVistas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaVista> ReservasVista { get; set; }
        public DbSet<Facturacion> Facturaciones { get; set; }
        public DbSet<TipoMovilidad> TipoMovilidad { get; set; }
        public DbSet<FiltroCoche> FiltroCoches { get; set; }

    }
}

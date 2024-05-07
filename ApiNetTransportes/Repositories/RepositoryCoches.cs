#region VISTAS and PROCEDURE
//alter view v_coches_lista
//as
//	select coche.idcoche, coche.imagen,
//    coche.asientos, coche.maletas,
//    coche.puertas, coche.precio,
//    coche.estadocoche,
//    modelo.nombre as model, puntuacion.puntuacion,
//    coche.idprovincia, tipomovilidad.nombre as tipo_cambio
//	from coche
//	inner join modelo
//	on  coche.idmodelo=  modelo.idmodelo
//	inner join tipomovilidad
//	on  coche.tipomovilidad=  tipomovilidad.idtipo
//	left join puntuacion
//	on coche.idpuntuacion = puntuacion.idpuntuacion
//go
//alter procedure sp_all_coches
//as
//	select * from v_coches_lista
//go


//create view v_all_usuarios
//as
//	select usuarios.idusuario, usuarios.nombre, usuarios.apellido,
//    usuarios.correo, usuarios.salt, usuarios.pass, usuarios.telefono,
//    roles.nombre as rol, estadousuario.nombre as estado

//	from usuarios
//	inner join estadousuario
//	on usuarios.estado  = estadousuario.idestado
//	inner join roles
//	on usuarios.idrol = roles.idrol
//go


//create procedure sp_all_usuarios
//as
//	select * from v_all_usuarios
//go

//create view v_reservas_lista
//as
//	select reserva.idreserva, reserva.lugar,
//    reserva.conductor, reserva.horainicial, reserva.fecharecogida,
//    reserva.fechadevolucion,
//    reserva.horafinal, coche.idcoche as coche, usuarios.nombre,
//    estadoreserva.nombre as reserva
//	from reserva
//	left join coche
//	on  reserva.idcoche=  coche.idcoche
//	left join usuarios
//	on  reserva.idusuario=  usuarios.idusuario
//	left join estadoreserva
//	on reserva.idestadoreserva = estadoreserva.idestado
//go
//create procedure sp_all_reservas
//as
//	select * from v_reservas_lista
//go

#endregion
using ApiNetTransportes.Data;
using ApiNetTransportes.Helpers;
using ApiNetTransportes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiNetTransportes.Repositories
{
    public class RepositoryCoches
    {
        private TransportesContext context;
        private HelperUploadFiles helperUploadFiles;
        public RepositoryCoches(TransportesContext context, HelperUploadFiles helperUploadFiles)
        {
            this.context = context;
            this.helperUploadFiles = helperUploadFiles;

        }
        #region COCHES
        public async Task<List<CocheVista>> GetCoches()
        {
            string sql = "SP_ALL_COCHES";
            var consulta = this.context.CocheVistas.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<List<Provincia>> GetProvincias()
        {
            List<Provincia> provincias = await this.context.Provincias.ToListAsync();
            return provincias;
        }
        public async Task<List<CocheVista>> CochesDispo()
        {
            return await this.context.CocheVistas.Where(x => x.EstadoCoche == true).ToListAsync();
             
        }
        public async Task<Coche> FindCoche(int id)
        {
            return await this.context.Coches.FirstOrDefaultAsync(z => z.IdCoche.Equals(id));
        }
        public async Task<CocheVista> FindCocheVista(int id)
        {
            return await this.context.CocheVistas.Where(z => z.IdCoche.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task DeleteCocheAsync(int id)
        {
            string sql = "DELETE FROM COCHE WHERE IDCOCHE = @IDCOCHE";
            SqlParameter pamId = new SqlParameter("@IDCOCHE", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }


        #endregion
        #region USUARIOS
        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(Z => Z.IdUsuario) + 1;
            }
        }
        public async Task<Usuario> RegisterUserAsync(string nombre, string apellido, string email, string password, int telefono)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.Correo = email;
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            user.Telefono = telefono;
            user.IdRol = 2;
            user.IdFacturacion = 1;
            user.EstadoUsuario = 1;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
            return user;
        }
        public async Task<Usuario> LoginUserAsync(string email, string password)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Correo == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response = HelperTools.CompareArrays(temp, passUser);
                if (response == true)

                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Usuario> FindUsuario(int id)
        {
         
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario.Equals(id)); ;
        }
        #endregion

        #region ADMIN
        public async Task<List<UsuarioVista>> GetUsuarios()
        {
            string sql = "SP_ALL_USUARIOS";
            var consulta = this.context.UsuarioVistas.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task EditarUsuario(int id, string nombre, string apellido, string correo, string pass, int telefono, int idFacturacicon)
        {
            Usuario user = await this.FindUsuario(id);
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.Correo = correo;
            string salt = HelperTools.GenerateSalt();
            byte[] password = HelperCryptography.EncryptPassword(pass, salt);
            user.Salt = salt;
            user.Password = password;
            user.Telefono = telefono;
            user.IdFacturacion = idFacturacicon;
            user.EstadoUsuario = 1;
            await context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            string sql = "DELETE FROM USUARIOS WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
        public async Task<List<Reserva>> GetRervas()
        {
            List<Reserva> reservas = this.context.Reservas.ToList();
            return reservas;
        }
        public async Task<List<Marca>> GetMarcas()
        {
            List<Marca> marcas = this.context.Marcas.ToList();
            return marcas;
        }
        public async Task<List<Modelo>> GetModelos()
        {
            List<Modelo> modelos = this.context.Modelos.ToList();
            return modelos;
        }
        public async Task<List<FiltroCoche>> GetFiltroCoches()
        {
            List<FiltroCoche> filtroCoches = this.context.FiltroCoches.ToList();
            return filtroCoches;
        }
        public async Task<List<TipoMovilidad>> GetTipoMovilidad()
        {
            List<TipoMovilidad> tipoMovilidad = this.context.TipoMovilidad.ToList();
            return tipoMovilidad;
        }
        private async Task<int> GetMaxIdCocheAsync()
        {
            if (this.context.Coches.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Coches.MaxAsync(Z => Z.IdCoche) + 1;
            }
        }


       
        public async Task<Coche> CrearCocheAsync(int modelo, int? valoracion, int tipomovi, int filtrocoche, int provincia, int asientos, int maletas, int puertas, int precio)
        {


            Coche coche = new Coche();
            coche.IdCoche = await this.GetMaxIdCocheAsync();
            coche.IdModelo = modelo;
            coche.Puntuacion = valoracion;
            coche.TipoMovilidad = tipomovi;
            coche.Filtro = filtrocoche;
       //     coche.Imagen = await this.helperUploadFiles.UploadFileAsync(, Folders.Uploads, coche.IdCoche); ;
            coche.EstadoCoche = true;
            coche.IdProvincia = provincia;
            coche.Asientos = asientos;
            coche.Maletas = maletas;
            coche.Puertas = puertas;
            coche.Precio = precio;
            this.context.Coches.AddAsync(coche);
            await this.context.SaveChangesAsync();
            return coche;
        }
        #endregion
        #region RESERVAS
        private async Task<int> GetMaxIdReservaAsync()
        {
            if (this.context.Reservas.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Reservas.MaxAsync(Z => Z.IdReserva) + 1;
            }
        }
        public async Task<List<ReservaVista>> GetReservas()
        {
            string sql = "sp_all_reservas";
            var consulta = this.context.ReservasVista.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<Reserva> CrearReservaAsync(string lugar, string conductor, TimeSpan horainit, DateTime fechainit
            , DateTime fechafinal, TimeSpan horafinal, int idcoche, int idusuario)
        {
            Reserva reser = new Reserva();
            reser.IdReserva = await this.GetMaxIdReservaAsync();
            reser.Lugar = lugar;
            reser.Conductor = conductor;
            reser.HoraInicial = horainit;
            reser.FechaRecogida = fechainit;
            reser.FechaDevolucion = fechafinal;
            reser.HoraFinal = horafinal;
            reser.IdCoche = idcoche;
            reser.IdUsuario = idusuario;
            reser.IdEstadpReserva = 1;
            this.context.Reservas.Add(reser);
            await this.context.SaveChangesAsync();
            return reser;
        }

        private async Task<int> GetMaxFacturacionAsync()
        {
            if (this.context.Facturaciones.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Facturaciones.MaxAsync(Z => Z.IdFacturacion) + 1;
            }
        }
        public async Task CrearFacturacionAsync(string nombre, string direccion, string ciudad, int codigopostal, string pais)
        {
            Facturacion fact = new Facturacion();
            fact.IdFacturacion = await this.GetMaxFacturacionAsync();
            fact.Nombre = nombre;
            fact.Direccion = direccion;
            fact.Ciudad = ciudad;
            fact.CodigoPostal = codigopostal;
            fact.Pais = pais;

            this.context.Facturaciones.Add(fact);
            await this.context.SaveChangesAsync();
        }
        [HttpPost
            ]
        public async Task<List<ReservaVista>> BuscadorReservas(string buscarReservas)
        {
            List<ReservaVista> reservas = await this.context.ReservasVista.Where(x => x.NombreUsuario.Contains(buscarReservas)).ToListAsync();

            return reservas;

        }
        public async Task<ReservaVista> FindReserva(int id)
        {
            return await this.context.ReservasVista.FirstOrDefaultAsync(z => z.IdReserva.Equals(id));
        }
        public async Task<Reserva> FindReservaAsync(int id)
        {
            return await this.context.Reservas.Where(z => z.IdReserva.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task CancelarReservaAsync(int id)
        {
            Reserva reserva = await this.context.Reservas.Where(z => z.IdReserva.Equals(id)).FirstOrDefaultAsync();
            reserva.IdEstadpReserva = 3;
            await this.context.SaveChangesAsync();

        }


        public async Task DeleteReservaAsync(int id)
        {
            string sql = "DELETE FROM RESERVA WHERE IDRESERVA = @IDRESERVA";
            SqlParameter pamId = new SqlParameter("@IDRESERVA", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }

        #endregion
    }
}

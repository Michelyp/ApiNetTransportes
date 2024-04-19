using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private RepositoryCoches repo;

        public ReservasController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        // GET: api/reservas
        /// <summary>
        /// Obtiene el conjunto de RESERVAS, tabla RESERVA.
        /// </summary>
        /// <remarks>
        /// Método para devolver todos las RESERVAS de la BBDD
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>   
        [HttpGet]
        public async Task<ActionResult<List<ReservaVista>>> Get()
        {
            return await this.repo.GetReservas();
        }
        // PUT: api/reservas
        /// <summary>
        /// Modifica una Reserva en la BBDD mediante su ID, tabla RESERVA
        /// </summary>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>/// 
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> CancelarReserva(int id)
        {
             await this.repo.CancelarReservaAsync(id);
            return Ok();
        }
        // GET: api/reservas/FindReservasAsync/{nombre}
        /// <summary>
        /// Obtiene conjunto de RESERVAVISTA , tabla RESERVAVISTA.
        /// </summary>
        /// <remarks>
        /// Permite buscar un objeto RESERVAVISTA por NOMBRE.  Tabla Relacional RESERVAVISTA
        /// </remarks>
        /// <param name="nombre">Nombre del usuario de la reserva</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [HttpGet]
        [Route("[action]/{nombre}")]
        public async Task<ActionResult<List<ReservaVista>>>
            FindReservasAsync(string nombre)
        {
            return await this.repo.BuscadorReservas(nombre);
        }

        // POST: api/reservas
        /// <summary>
        /// Crea una nueva RESERVA en la BBDD, tabla RESERVA
        /// </summary>
        /// <remarks>
        /// Este método inserta una nueva RESERVA enviando el Objeto JSON
        /// El ID de la charla se genera automáticamente dentro del método
        /// </remarks>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>///     
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Reserva>> CrearReserva(ReservaModel reserva)
        {
            //recoge mediante Claims los datos del usuario 
            Claim claimUser = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonUser = claimUser.Value;
            Usuario user = JsonConvert.DeserializeObject<Usuario>(jsonUser);
            int idUser = user.IdUsuario;
            Reserva reserv = await this.repo.CrearReservaAsync(reserva.Lugar, reserva.Conductor, reserva.HoraInicial,
                reserva.FechaRecogida, reserva.FechaDevolucion, reserva.HoraFinal, reserva.IdCoche, idUser);
            if (reserv == null)
            {
                return NotFound();
            }
            return reserv;
        }

        // DELETE: api/reservas/{id}
        /// <summary>
        /// Elimina una reserva en la BBDD mediante su ID. Tabla RESERVA
        /// </summary>
        /// <remarks>
        /// Enviaremos el ID mediante la URL
        /// </remarks>
        /// <param name="id">ID de la RESERVA a eliminar</param>
        /// <response code="201">Deleted. Objeto eliminado en la BBDD.</response> 
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        /// <response code="500">BBDD. No se ha eliminado el objeto en la BD. Error en la BBDD.</response>/// 

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReserva(int id)
        {
            var reserva = await this.repo.FindReserva(id);
            if (reserva == null) { return NotFound(); }
            await this.repo.DeleteReservaAsync(id);
            return Ok();
        }
    }
}

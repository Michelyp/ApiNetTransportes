using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private RepositoryCoches repo;
        public AdministradorController(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        //Método por verificar 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="valoracion"></param>
        /// <param name="tipomovi"></param>
        /// <param name="filtrocoche"></param>
        /// <param name="imagen"></param>
        /// <param name="provincia"></param>
        /// <param name="asientos"></param>
        /// <param name="maletas"></param>
        /// <param name="puertas"></param>
        /// <param name="precio"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ActionResult> AgregarCoche(Coche coche)
        {
            await this.repo.CrearCocheAsync(coche.IdModelo, coche.Puntuacion, coche.TipoMovilidad, coche.Filtro,  coche.IdProvincia, coche.Asientos, coche.Maletas,
                coche.Puertas, coche.Precio);

            return RedirectToAction("Coches", "Coches");

        }
        // GET: api/cursos/{id}
        /// <summary>
        /// Obtiene un Curso por su Id, tabla CURSOS.
        /// </summary>
        /// <remarks>
        /// Permite buscar un objeto Curso por su ID
        /// </remarks>
        /// <param name="id">Id (GUID) del objeto CURSO.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="telefono"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> CrearUsuario(UsuarioModel usuario)
        {
            Usuario user = await this.repo.RegisterUserAsync(usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.Password, usuario.Telefono);
            if (user == null){
                return NotFound();
            }
            return user;
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> EditarUsuario(UsuarioModelEditar usuario)

        {
           await this.repo.EditarUsuario(usuario.IdUsuario,usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.Password, usuario.Telefono, usuario.IdFacturacion);
            return Ok();
      
        }

    }
}

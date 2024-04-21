using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryCoches repo;
        public UsuariosController(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        // GET: api/usuarios
        /// <summary>
        /// Obtiene el conjunto de USUARIOS, tabla USUARIOS.
        /// </summary>
        /// <remarks>
        /// Método para devolver todos las USUARIOS de la BBDD
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>      
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UsuarioVista>>> GetVistaUsuarios()
        {
            return await this.repo.GetUsuarios();
        }

        // POST: api/usuarios
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
        [Authorize]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> CrearUsuario(UsuarioModel usuario)
        {
            Usuario user = await this.repo.RegisterUserAsync(usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.Password, usuario.Telefono);
            if (user == null){
                return NotFound();
            }
            return user;
        }

        // PUT: api/usuarios
        /// <summary>
        /// Modifica una USUARIOS en la BBDD mediante su ID, tabla USUARIOS
        /// </summary>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>/// 
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Usuario>> EditarUsuario(UsuarioModelEditar usuario)

        {
           await this.repo.EditarUsuario(usuario.IdUsuario,usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.Password, usuario.Telefono, usuario.IdFacturacion);
            return Ok();
        }

        // DELETE: api/usuarios/{id}
        /// <summary>
        /// Elimina una USUARIOS en la BBDD mediante su ID. Tabla USUARIOS
        /// </summary>
        /// <remarks>
        /// Enviaremos el ID mediante la URL
        /// </remarks>
        /// <param name="id">ID del USUARIOS a eliminar</param>
        /// <response code="201">Deleted. Objeto eliminado en la BBDD.</response> 
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        /// <response code="500">BBDD. No se ha eliminado el objeto en la BD. Error en la BBDD.</response>/// 

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var user = await this.repo.FindUsuario(id);
            if(user == null) { return NotFound(); }
            await this.repo.DeleteUsuarioAsync(id);
            return Ok();
        }

        // GET: api/usuarios/perfilusuario
        /// <summary>
        /// Obtiene un USUARIO a partir de su TOKEN, tabla USUARIOS.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        /// <response code="401">NotAuthorized. No autorizado, sin Token válido.</response>     c
        [HttpGet]
        [Authorize]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Usuario> PerfilUsuario()
        {
            //recoge mediante Claims los datos del usuario 
            Claim claimUser = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonUser = claimUser.Value;
            Usuario user = JsonConvert.DeserializeObject<Usuario>(jsonUser);
            int idUser = user.IdUsuario;
            Usuario userValid = await this.repo.FindUsuario(idUser);
            return userValid;
        }

    }
}

using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using ApiNetTransportes.Helpers;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagedController : ControllerBase
    {
        private RepositoryCoches repo;
        private HelperToken helper;
        public ManagedController(RepositoryCoches repo, HelperToken helper)
        {
            this.helper = helper;
            this.repo = repo;
        }
        // POST: api/auth/login
        /// <summary>
        /// Obtiene un TOKEN con Email y Password de un Usuario
        /// </summary>
        /// <remarks>
        /// Incluir los siguientes datos: 
        /// Email: michelypintom@gmail.com, Password: 1234
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        /// <response code="401">NotAuthorized. No autorizado, sin Token válido.</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model )
        {
            Usuario user = await this.repo.LoginUserAsync(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                ///UN token qye dentro tendra las credenciales
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(),
                    SecurityAlgorithms.HmacSha256);
                string jsonUser = JsonConvert.SerializeObject(user);
                Claim[] infoUsuario = new[]
                {
                    new Claim("UserData", jsonUser)
                };
                //Generamos el token 
                //Este token tendra el issuer, audience,tiempo y otros
                  JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: infoUsuario,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );
                //DEVOLVEMOS UNA RESPUESTA CORRECTA CON EL TOKEN
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
        // POST: api/usuarios
        /// <summary>
        /// Crea una nueva USUARIOS en la BBDD, tabla USUARIOS
        /// </summary>
        /// <remarks>
        /// Este método inserta un nuevo USUARIOS enviando el Objeto JSON
        /// El ID de la charla se genera automáticamente dentro del método
        /// </remarks>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>/// 
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> Register(UsuarioModel usuario)
        {
           Usuario user=  await this.repo.RegisterUserAsync(usuario.Nombre, usuario.Apellido, usuario.Correo,usuario.Password,usuario.Telefono);
            return user;
        }
    }
}


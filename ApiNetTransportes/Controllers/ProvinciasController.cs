using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private RepositoryCoches repo;

        public ProvinciasController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        // GET: api/provincias
        /// <summary>
        /// Obtiene el conjunto de PROVINCIAS, tabla PROVINCIA.
        /// </summary>
        /// <remarks>
        /// Método para devolver todos las PROVINCIAS de la BBDD
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>   
        [HttpGet]
        public async Task<ActionResult<List<Provincia>>> GetProvincias()
        {
            return await this.repo.GetProvincias();
        }


    }
}

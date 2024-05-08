using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovilidadController : ControllerBase
    {
        private RepositoryCoches repo;
        public TipoMovilidadController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        // GET: api/coches
        /// <summary>
        /// Obtiene el conjunto de Coches con formato , tabla COCHEVISTA.
        /// </summary>
        /// <remarks>
        /// Método para devolver todas las Coches con formato de datos mapeados
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>   
        [HttpGet]
        public async Task<ActionResult<List<TipoMovilidad>>> Get()
        {
            return await this.repo.GetTipoMovilidad();
        }
    }
}

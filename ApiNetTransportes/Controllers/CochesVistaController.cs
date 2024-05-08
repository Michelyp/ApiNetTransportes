using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CochesVistaController : ControllerBase
    {
        private RepositoryCoches repo;
        public CochesVistaController(RepositoryCoches repo) { 
            this.repo = repo;
        }
        // GET: api/cochesvista/{id}
        /// <summary>
        /// Obtiene un COCHE por su Id, tabla V_COCHES_LISTA.
        /// </summary>
        /// <remarks>
        /// Permite buscar un objeto COCHEVISTA por su ID
        /// </remarks>
        /// <param name="id">Id (GUID) del objeto COCHEVISTA.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<CocheVista>> FindCocheVista(int id)
        {
            var coche = await this.repo.FindCocheVista(id);
            if (coche == null)
            {
                return NotFound();
            }
            return coche;
        }
        // GET: api/cochesvista/UsersFormato
        /// <summary>
        /// Obtiene el conjunto de COCHES con formato, tabla USERSFORMATOVIEW.
        /// </summary>
        /// <remarks>
        /// Método para devolver todos las COCHES con datos Formateados
        /// Necesario TOKEN
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>     
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CocheVista>>> GetCochesDisponible()
        {
            return await this.repo.CochesDispo();
        }
    }
}

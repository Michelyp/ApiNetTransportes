using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CochesController : ControllerBase
    {
        private RepositoryCoches repo;
        public CochesController(RepositoryCoches repo)
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
        public async Task<ActionResult<List<CocheVista>>> GetCoches()
        {
            return await this.repo.GetCoches();
        }
        // POST: api/coches
        /// <summary>
        /// Crea un nuevo coche en la BBDD, tabla COCHE
        /// </summary>
        /// <remarks>
        /// Este método inserta un nuevo EMPRESASCENTROS enviando el Objeto JSON
        /// El ID se genera automáticamente dentro del método
        /// </remarks>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>/// 

        [HttpPost]
        public async Task<ActionResult<Coche>> AgregarCoche(Coche coche)
        {
           Coche cocheAgree =  await this.repo.CrearCocheAsync(coche.IdModelo, coche.Puntuacion, coche.TipoMovilidad, coche.Filtro, coche.IdProvincia, coche.Asientos, coche.Maletas,
                coche.Puertas, coche.Precio);
            return cocheAgree;
        }
        // GET: api/coches/{id}
        /// <summary>
        /// Obtiene un COCHE por su Id, tabla COCHE.
        /// </summary>
        /// <remarks>
        /// Permite buscar un objeto COCHE por su ID
        /// </remarks>
        /// <param name="id">Id (GUID) del objeto Coche.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [HttpGet("{id}")]
        public async Task<ActionResult<Coche>> Find(int id)
        {
            var coche = await this.repo.FindCoche(id);
            if (coche == null)
            {
                return NotFound();
            }
            return coche;
        }
        // DELETE: api/coches/{id}
        /// <summary>
        /// Elimina un Coche en la BBDD mediante su ID. Tabla COCHE
        /// </summary>
        /// <remarks>
        /// Enviaremos el ID mediante la URL
        /// </remarks>
        /// <param name="id">ID de COCHE a eliminar</param>
        /// <response code="201">Deleted. Objeto eliminado en la BBDD.</response> 
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        /// <response code="500">BBDD. No se ha eliminado el objeto en la BD. Error en la BBDD.</response>/// 
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCoche(int id)
        {
            var coche = await this.repo.FindCoche(id);
            if (coche == null)
            {
                return NotFound();
            }
            await this.repo.DeleteCocheAsync(id);
            return Ok();
        }
    }
}

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
        [HttpGet]
        public async Task<ActionResult<List<CocheVista>>> GetCoches()
        {
            return await this.repo.GetCoches();
        }
        [HttpGet]
        [Route("[action]/({id})")]

        public async Task<ActionResult<Coche>> Find(int id)
        {
            var coche = await this.repo.FindCoche(id);
            if (coche == null)
            {
                return NotFound();
            }
            return coche;
        }
        [HttpGet]
        [Route("[action]/({id})")]

        public async Task<ActionResult<CocheVista>> FindCocheVista(int id)
        {
            var coche = await this.repo.FindCocheVista(id);
            if (coche == null)
            {
                return NotFound();
            }
            return coche;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CocheVista>>> GetCochesDisponible()
        {
            return await this.repo.CochesDispo();
        }
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

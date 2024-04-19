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

        [HttpPost]

        public async Task<IActionResult> AgregarCoche(int modelo, int? valoracion, int tipomovi, int filtrocoche, IFormFile imagen, int provincia, int asientos, int maletas, int puertas, int precio)
        {
            await this.repo.CrearCocheAsync(modelo, valoracion, tipomovi, filtrocoche, imagen, provincia, asientos, maletas, puertas, precio);

            return RedirectToAction("Coches", "Coches");

        }

    }
}

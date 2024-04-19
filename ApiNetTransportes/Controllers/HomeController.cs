using ApiNetTransportes.Models;
using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private RepositoryCoches repo;

        public HomeController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Provincia>>> GetProvincias()
        {
            return await this.repo.GetProvincias();
        }

    }
}

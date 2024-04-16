using ApiNetTransportes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetTransportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportesController : ControllerBase
    {
        private RepositoryCoches repo;
        public TransportesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }
    }
}

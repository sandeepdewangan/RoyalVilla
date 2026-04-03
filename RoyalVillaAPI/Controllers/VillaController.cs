using Microsoft.AspNetCore.Mvc;

namespace RoyalVillaAPI.Controllers
{
    [Route("/api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public string GetVillas()
        {
            return "Gotted all villas";
        }

        [HttpGet("{id:int}")]
        public string GetVillasById(int id)
        {
            return "Got villa no. " + id;
        }

    }
}

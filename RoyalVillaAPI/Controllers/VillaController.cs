using Microsoft.AspNetCore.Mvc;

namespace RoyalVillaAPI.Controllers
{
    [ApiController]
    public class VillaController : ControllerBase
    {
        // End Point
        [HttpGet]
        [Route("/villas")]
        public string GetVillas()
        {
            return "Gotted all villas";
        }
    }
}

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
        public string GetVillas(int id)
        {
            return "Got villa no. " + id;
        }

        // Here name is by default string type, no need to define
        // By default the parameters are [FromRequest]
        // https://localhost:7138/api/villa/{id}/{name}


        [HttpGet("{id:int}/{name}")]
        public string GetVillas(int id, string name)
        {
            return "Got villa no. " + id + name;
        }

        // It will get parameters from query parameters
        // https://localhost:7138/api/villa?id=2&name=sandeep
        [HttpGet]
        public string GetVillasFromQuery([FromQuery] int id, [FromQuery] string name)
        {
            return "Got villa no. " + id + name;
        }
        // Similarly we can have parameters from header using [FromHeader]
    }
}

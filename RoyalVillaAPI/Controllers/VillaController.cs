using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVillaAPI.Data;
using RoyalVillaAPI.Data.Models;
using RoyalVillaAPI.Data.Models.DTO;

namespace RoyalVillaAPI.Controllers
{
    [Route("/api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<VillaDTO>>>> GetVillas()
        {
            // We have returned VillaDTO because in practice we never return database entity directly.
            var villas = await _db.Villa.ToListAsync();
            var villaDTOResponse = _mapper.Map<List<VillaDTO>>(villas);
            var response = ApiResponse<IEnumerable<VillaDTO>>.Ok(villaDTOResponse, "Villa Retrieved Successfully");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<Villa>>> GetVillasById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    // Consistant API Response
                    return BadRequest(ApiResponse<object>.BadMessage("Villa Id must be greater than 0"));
                    //return new ApiResponse<Villa>()
                    //{
                    //    StatusCode = 400,
                    //    Errors = "Villa Id must be greater than 0",
                    //    Success = false,
                    //    Message = "Bad Request"
                    //};
                }

                var villa = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (villa == null)
                {
                    //return NotFound($"Villa with Id: {id} is not found!");
                    return NotFound(ApiResponse<object>.NotFound($"Villa with Id: {id} is not found!"));

                }
                return Ok(ApiResponse<Villa>.Ok(villa, "Record retrived successfully!"));
            }
            catch (Exception ex)
            {
                var error = ApiResponse<object>.Error(500, "An Error has occured while retrieving villa", ex.Message);
                return StatusCode(500, error);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Villa>>> CreateVilla(VillaCreateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    return BadRequest(ApiResponse<object>.BadMessage("Villa data is required"));
                    //return BadRequest("Villa data is required.");
                }

                // Check for villa name already exists or not
                var duplicateVilla = await _db.Villa.FirstOrDefaultAsync(x => x.Name.ToLower() == villaDto.Name.ToLower());
                if (duplicateVilla != null)
                {
                    //return Conflict($"Villa with {villaDto.Name} already exists.");
                    return Conflict(ApiResponse<Villa>.Conflict($"Villa with {villaDto.Name} already exists."));
                }

                Villa villa = _mapper.Map<Villa>(villaDto);
                villa.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                //Villa villa = new Villa()
                //{
                //    Name = villaDto.Name,
                //    Details = villaDto.Details,
                //    ImageUrl = villaDto.ImageUrl,
                //    Occupancy = villaDto.Occupancy,
                //    Sqft = villaDto.Sqft,
                //    Rate = villaDto.Rate,
                //    CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                //};
                await _db.Villa.AddAsync(villa);
                await _db.SaveChangesAsync();

                // Params:
                // 1: actionName-from where we can access the resouce.
                // 2: routeValue-used to create url api/villa/1
                // 3: otherValues

                // Status Code: 201 Created
                // Location Header: URL of the new resource
                // Response Body: the created object(villa)

                var response = ApiResponse<Villa>.CreatedAt(data: villa, message: "Villa created successfully!");
                return CreatedAtAction(nameof(GetVillasById), new { id = villa.Id }, response);
            }
            catch (Exception ex)
            {
                var error = ApiResponse<object>.Error(500, "An Error has occured while creating villa", ex.Message);
                return StatusCode(500, error);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<Villa>>> UpdateVilla(int id, VillaUpdateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    //return BadRequest("Villa data is required.");
                    return BadRequest(ApiResponse<object>.BadMessage("Villa data is required"));
                }
                if (id != villaDto.Id)
                {
                    //return BadRequest("Villa Id in URL doesnot matched with request body.");
                    return BadRequest(ApiResponse<object>.BadMessage("Villa Id in URL doesnot matched with request body."));
                }
                var existingVilla = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (existingVilla == null)
                {
                    //return NotFound($"Villa with {id} was not found");
                    return NotFound(ApiResponse<object>.NotFound($"Villa with {id} was not found"));
                }

                _mapper.Map(villaDto, existingVilla);
                existingVilla.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                await _db.SaveChangesAsync();

                return Ok(villaDto);
            }
            catch (Exception ex)
            {
                var error = ApiResponse<object>.Error(500, "An Error has occured while updating villa", ex.Message);
                return StatusCode(500, error);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteVilla(int id)
        {
            try
            {
                var existingVilla = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (existingVilla == null)
                {
                    //return NotFound($"Villa with {id} was not found");
                    return NotFound(ApiResponse<object>.NotFound($"Villa with {id} was not found"));
                }
                _db.Villa.Remove(existingVilla);
                await _db.SaveChangesAsync();

                var response = ApiResponse<object>.NoContent("Villa Deleted Successfully!");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ApiResponse<object>.Error(500, "An Error has occured while deleting villa", ex.Message);
                return StatusCode(500, error);
            }
        }
    }
}

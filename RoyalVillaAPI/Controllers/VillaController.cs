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
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
        {
            return Ok(await _db.Villa.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Villa>> GetVillasById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Villa Id must be greater than 0");
                }

                var villa = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound($"Villa with Id: {id} is not found!");
                }
                return Ok(villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An Error has occured while retrieving villa with Id: {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Villa>> CreateVilla(VillaCreateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    return BadRequest("Villa data is required.");
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

                return CreatedAtAction(nameof(GetVillasById), new { id = villa.Id }, villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An Error has occured while creating villa: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Villa>> UpdateVilla(int id, VillaUpdateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    return BadRequest("Villa data is required.");
                }
                if (id != villaDto.Id)
                {
                    return BadRequest("Villa Id in URL doesnot matched with request body.");
                }
                var existingVilla = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (existingVilla == null)
                {
                    return NotFound($"Villa with {id} was not found");
                }

                _mapper.Map(villaDto, existingVilla);
                existingVilla.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                await _db.SaveChangesAsync();

                return Ok(villaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An Error has occured while updating villa: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Villa>> DeleteVilla(int id)
        {
            try
            {
                var existingVilla = await _db.Villa.FirstOrDefaultAsync(x => x.Id == id);
                if (existingVilla == null)
                {
                    return NotFound($"Villa with {id} was not found");
                }
                _db.Villa.Remove(existingVilla);
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An Error has occured while deleting villa: {ex.Message}");
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinemakerAPI.DbContexts;
using WinemakerAPI.Entities;
using WinemakerAPI.Models;

namespace WinemakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineBottlesController : ControllerBase
    {
        private readonly WineCollectionContext _context;
        private readonly IMapper _mapper;

        public WineBottlesController(WineCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all wine bottles along with their associated wine maker information.
        /// </summary>
        /// <returns>A list of wine bottles, or an error response if something goes wrong.</returns>
        /// <response code="200">Returns a list of wine bottles</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetWineBottle>>> GetWineBottles(
            [FromQuery] string name = null,
            [FromQuery] int? year = null,
            [FromQuery] string style = null,
            [FromQuery] string taste = null,
            [FromQuery] int? minCount = null,
            [FromQuery] int? maxCount = null)
        {
            var query = _context.WineBottles.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(w => w.Name.Contains(name)); 
            }
            if (year.HasValue)
            {
                query = query.Where(w => w.Year == year.Value);
            }
            if (!string.IsNullOrEmpty(style))
            {
                query = query.Where(w => w.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(taste))
            {
                query = query.Where(w => w.Name.Contains(name));
            }
            if (minCount.HasValue)
            {
                query = query.Where(w => w.CountInWineCellar >= minCount.Value);
            }
            if (maxCount.HasValue)
            {
                query = query.Where(w => w.CountInWineCellar <= maxCount.Value);
            }

            var wineBottles = await query.Include(w => w.WineMaker).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<GetWineBottle>>(wineBottles));
        }

        /// <summary>
        /// Retrieves a specific wine bottle by its ID along with its associated wine maker information.
        /// </summary>
        /// <param name="id">The unique identifier of the wine bottle.</param>
        /// <returns>The wine bottle details, Otherwise a 404 error if the wine bottle is not found.</returns>
        /// <response code="200">Returns the wine bottle details if found</response>
        /// <response code="404">If the wine bottle with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWineBottle>> GetWineBottle([FromQuery] int id)
        {
            var wineBottle = await _context.WineBottles
                .Include(wb => wb.WineMaker)
                .FirstOrDefaultAsync(wb => wb.Id == id);

            if (wineBottle is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetWineBottle>(wineBottle));
        }

        /// <summary>
        /// Creates a new wine bottle in the system.
        /// </summary>
        /// <param name="postWineBottle">The data for the new wine bottle to be created.</param>
        /// <returns>A newly created wine bottle with a 201 Created status if successful, Otherwise a 400 Bad Request if the data is invalid.</returns>
        /// <response code="201">Returns the newly created wine bottle</response>
        /// <response code="400">If the input data is null or the WineMakerId is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WineBottle>> PostWineBottle(PostWineBottle postWineBottle)
        {
            if (postWineBottle == null)
            {
                return BadRequest("WineBottle data is null.");
            }

            var wineBottle = _mapper.Map<WineBottle>(postWineBottle);

            var wineMakerExists = await _context.WineMakers.AnyAsync(wm => wm.Id == wineBottle.WineMakerId);
            if (!wineMakerExists)
            {
                return BadRequest("Invalid WineMakerId.");
            }

            _context.WineBottles.Add(wineBottle);
            await _context.SaveChangesAsync();

            // Map back to DTO
            var result = _mapper.Map<WineBottle>(wineBottle);

            return CreatedAtAction(nameof(GetWineBottle), new { id = result.Id }, result);
        }


        /// <summary>
        /// Deletes a specific wine bottle from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the wine bottle to be deleted.</param>
        /// <returns>No content if the deletion is successful, or a 404 Not Found if the wine bottle does not exist.</returns>
        /// <response code="204">If the wine bottle was successfully deleted</response>
        /// <response code="404">If the wine bottle with the specified ID was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWineBottle([FromQuery] int id)
        {
            var wineBottle = await _context.WineBottles.FindAsync(id);

            if (wineBottle is null)
            {
                return NotFound();
            }

            _context.WineBottles.Remove(wineBottle); 
            await _context.SaveChangesAsync(); 

            return NoContent(); 
        }

        /// <summary>
        /// Updates a specific wine bottle by id.
        /// </summary>
        /// <param name="id">The id of the wine bottle to update.</param>
        /// <param name="updatedWineBottle">The updated wine bottle data.</param>
        /// <returns> A status code indicating the result of the update operation </returns>
        /// <response code="204"> If update was successful.</response>
        /// <response code="400"> If The request is invalid. either because missmatch between id of wine bottle id from body and from param
        /// or because of maker id does not exist in the database </response>
        /// <response code="404"> Not Found The wine bottle with the specified id does not exist in the database.</response> 

        [HttpPut("{id}")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> UpdateWineBottle(int id, UpdateWineBottle updatedWineBottle)
        {
            if (id != updatedWineBottle.Id)
            {
                return BadRequest("The wine bottle you requested to update does not match the id in param."); 
            }

            var existingWineBottle = await _context.WineBottles.FindAsync(id);
            if (existingWineBottle is null)
            {
                return NotFound();
            }

            if (existingWineBottle.WineMakerId != updatedWineBottle.WineMakerId)
            {
                var existingWineMaker = await _context.WineMakers.FindAsync(updatedWineBottle.WineMakerId);

                if (existingWineMaker == null)
                {
                    return BadRequest("The wine maker of this wine bottle does not exist."); 
                }
            }

            // Update the existing entity with the new values
            existingWineBottle.Name = updatedWineBottle.Name;
            existingWineBottle.Year = updatedWineBottle.Year;
            existingWineBottle.Size = updatedWineBottle.Size;
            existingWineBottle.CountInWineCellar = updatedWineBottle.CountInWineCellar;
            existingWineBottle.Style = updatedWineBottle.Style;
            existingWineBottle.Taste = updatedWineBottle.Taste;
            existingWineBottle.Description = updatedWineBottle.Description;
            existingWineBottle.FoodPairing = updatedWineBottle.FoodPairing;
            existingWineBottle.Link = updatedWineBottle.Link;
            existingWineBottle.Image = updatedWineBottle.Image;
            existingWineBottle.WineMakerId = updatedWineBottle.WineMakerId;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }
    }
}

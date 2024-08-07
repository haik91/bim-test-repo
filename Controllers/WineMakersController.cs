using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinemakerAPI.DbContexts;
using WinemakerAPI.Models;
using WinemakerAPI.Entities;

namespace WinemakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineMakersController : ControllerBase
    {
        private readonly WineCollectionContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the WineMakersController class.
        /// </summary>
        /// <param name="context">The database context used for accessing wine maker data.</param>
        /// <param name="mapper">The object mapper used for mapping between domain models and DTOs.</param>
        public WineMakersController(WineCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all wine makers from the database.
        /// </summary>
        /// <returns>
        /// Returns a 200 OK status code with a list of wine makers if successful.
        /// </returns>
        /// <response code="200">Returns the list of all wine makers.</response>
        /// <response code="500">Returns an error if there is a problem with retrieving the data.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetWineMaker>>> GetWinemakers()
        {
            var allWineMakers  = await _context.WineMakers.Include(w => w.WineBottles).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<GetWineMaker>>(allWineMakers));
        }

        /// <summary>
        /// Retrieves a specific wine maker by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the wine maker to retrieve.</param>
        /// <returns>
        /// Returns the wine maker details if found. Otherwise returns code 404.
        /// </returns>
        /// <response code="200">Returns the details of the requested wine maker.</response>
        /// <response code="404">Returns a not found response if the wine maker with the specified ID does not exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWineMaker>> GetWinemaker(int id)
        {
            var wineMaker = await _context.WineMakers.Include(w => w.WineBottles)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (wineMaker is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetWineMaker>(wineMaker));
        }

        /// <summary>
        /// Creates a new wine maker in the system.
        /// </summary>
        /// <param name="postWineMaker">The data transfer object containing the details of the wine maker to be created.</param>
        /// <returns>
        /// Returns the newly created wine maker and its details if successful.
        /// Otherwise returns a 400 Bad Request status code either if the input data is invalid or if the wine maker already exists.
        /// </returns>
        /// <response code="201">Returns the details of the newly created wine maker and the location where it can be retrieved.</response>
        /// <response code="400">Returns a bad request response if the wine maker is null or if a wine maker with the same name already exists.</response>
        [HttpPost]
        public async Task<ActionResult<GetWineMaker>> PostWineMaker(PostWineMaker postWineMaker)
        {
            if (postWineMaker is null)
            {
                return BadRequest("Wine maker is empty");
            }

            var exisitingWinemakers = _context.WineMakers.Where(w => w.Name == postWineMaker.Name).ToList();

            if(exisitingWinemakers.Any())
            {
                return BadRequest("Wine maker already exists");
            }

            var wineMaker = _mapper.Map<WineMaker>(postWineMaker);
            _context.WineMakers.Add(wineMaker);
            await _context.SaveChangesAsync();

            var wineMakerDto = _mapper.Map<GetWineMaker>(wineMaker);
            return CreatedAtAction(nameof(GetWinemaker), new { id = wineMaker.Id }, wineMaker);
        }

        /// <summary>
        /// Deletes a wine maker by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the wine maker to be deleted.</param>
        /// <returns>
        /// Returns a 204 No Content status code if the deletion was successful.
        /// Returns a 404 Not Found status code if no wine maker with the specified ID was found.
        /// </returns>
        /// <response code="204">Returns a no content response indicating that the deletion was successful.</response>
        /// <response code="404">Returns a not found response if no wine maker with the specified ID exists.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWinemaker(int id)
        {
            var wineMaker = await _context.WineMakers.FindAsync(id);

            if(wineMaker is null)
            {
                return NotFound();
            }

            _context.WineMakers.Remove(wineMaker);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

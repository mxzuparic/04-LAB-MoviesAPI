using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MoviesService _moviesService;

        public MovieController(MoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAll()
        {
            var movies = await _moviesService.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _moviesService.GetByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Add(Movie movie)
        {
            var addedMovie = await _moviesService.AddAsync(movie);
            return CreatedAtAction(nameof(GetById), new { id = addedMovie.Id }, addedMovie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Movie movie)
        {
            if (id != movie.Id) return BadRequest();

            var updated = await _moviesService.UpdateAsync(movie);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _moviesService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

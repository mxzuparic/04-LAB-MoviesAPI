using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;

namespace MoviesAPI.Services
{
    public class MoviesService
    {
        private readonly MoviesContext _context;

        public MoviesService(MoviesContext context)
        {
            _context = context;
        }
        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }
        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }
        public async Task<Movie> AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        public async Task<bool> UpdateAsync(Movie movie)
        {
            var existing = await _context.Movies.FindAsync(movie.Id);
            if (existing == null) return false;

            existing.Name = movie.Name;
            existing.Year = movie.Year;
            existing.Genre = movie.Genre;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

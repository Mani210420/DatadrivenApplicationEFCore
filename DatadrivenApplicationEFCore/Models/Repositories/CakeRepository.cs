
using Microsoft.EntityFrameworkCore;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly DatadrivenApplicationDbContext _context;

        public CakeRepository(DatadrivenApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cake>> GetAllCakesAsync()
        {
            return await _context.Cakes.OrderBy(c => c.CakeId).ToListAsync();
        }

        public async Task<Cake?> GetCakeByIdAsync(int id)
        {
            return await _context.Cakes.Include(c => c.Ingredients).Include(c => c.Category).FirstOrDefaultAsync(c => c.CakeId == id);
        }
    }
}

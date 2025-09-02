using Microsoft.EntityFrameworkCore;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatadrivenApplicationDbContext _context;

        public CategoryRepository(DatadrivenApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.CategoryId).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Cakes).FirstOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}

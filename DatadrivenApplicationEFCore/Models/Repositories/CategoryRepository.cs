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

        public async Task<int> AddCategoryAsync(Category category)
        {
            bool EnteredCategoryExists =await _context.Categories.AnyAsync(c => c.Name == category.Name);

            if(EnteredCategoryExists)
            {
                throw new Exception("Category with same name already exists");
            }

            _context.Categories.Add(category);
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            //throw new Exception("Slow Database");
            return await _context.Categories.OrderBy(c => c.CategoryId).AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Cakes).AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<int> UpdateCategory(Category category)
        {
            bool sameCategoryExists =await _context.Categories.AnyAsync(c => c.Name == category.Name && c.CategoryId != category.CategoryId);
            if(sameCategoryExists)
            {
                throw new Exception("Same category name already exists");
            }

            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if (categoryToUpdate != null) 
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.CategoryId = category.CategoryId;
                categoryToUpdate.DateAdded = category.DateAdded;

                _context.Categories.Update(categoryToUpdate);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}

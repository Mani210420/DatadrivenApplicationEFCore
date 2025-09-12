using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatadrivenApplicationDbContext _context;
        private IMemoryCache _memoryCache;
        private const string AllCategoriesCacheName = "AllCategories";

        public CategoryRepository(DatadrivenApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            bool EnteredCategoryExists =await _context.Categories.AnyAsync(c => c.Name == category.Name);

            if(EnteredCategoryExists)
            {
                throw new Exception("Category with same name already exists");
            }

            _context.Categories.Add(category);
            int result = await _context.SaveChangesAsync();

            _memoryCache.Remove(AllCategoriesCacheName);
            return result;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            var cakesInCategory = _context.Cakes.Any(c => c.CategoryId == id);
            if (cakesInCategory)
            {
                throw new Exception("Please delete all cakes in this category before deleting the category...");
            }
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync( c => c.CategoryId == id);
            if (categoryToDelete != null) 
            {
                _context.Categories.Remove(categoryToDelete);
                return await _context.SaveChangesAsync();
            }
            else 
            {
                throw new ArgumentException($"The category cannot be deleted");
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            //throw new Exception("Slow Database");
            //return await _context.Categories.OrderBy(c => c.CategoryId).AsNoTracking().ToListAsync();
            List<Category> allCategories = null;
            if(! _memoryCache.TryGetValue(AllCategoriesCacheName, out allCategories))
            {
                allCategories = await _context.Categories.AsNoTracking().OrderBy(c => c.CategoryId).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                _memoryCache.Set(AllCategoriesCacheName, allCategories,cacheEntryOptions);
            }

            return allCategories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Cakes).AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<int> UpdateCategoryAsync(Category category)
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

        public async Task<int> UpdateCategoryNamesAsync(List<Category> categories)
        {
            foreach (var category in categories)
            {
                var categoryToUpdate =await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);
                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = category.Name;
                    _context.Categories.Update(categoryToUpdate);
                }
            }

            int res = await _context.SaveChangesAsync();

            _memoryCache.Remove(AllCategoriesCacheName);

            return res;
        }
    }
}


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
            return await _context.Cakes.OrderBy(c => c.CakeId).AsNoTracking().ToListAsync();
        }

        public async Task<Cake?> GetCakeByIdAsync(int id)
        {
            return await _context.Cakes.Include(c => c.Ingredients).Include(c => c.Category).AsNoTracking().FirstOrDefaultAsync(c => c.CakeId == id);
        }

        public async Task<int> AddCakeAsync(Cake cake)
        {
            _context.Cakes.Add(cake);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCakeAsync(Cake cake)
        {
            var selectedCake = await _context.Cakes.FirstOrDefaultAsync(c => c.CakeId == cake.CakeId);
            if (selectedCake != null) 
            {
                selectedCake.CategoryId = cake.CategoryId;
                selectedCake.ShortDescription = cake.ShortDescription;
                selectedCake.LongDescription = cake.LongDescription;
                selectedCake.Price = cake.Price;
                selectedCake.AllergyInfo = cake.AllergyInfo;
                selectedCake.ImageThumbnail = cake.ImageThumbnail;
                selectedCake.ImageUrl = cake.ImageUrl;
                selectedCake.InStock = cake.InStock;
                selectedCake.IsCakeOfTheWeek = cake.IsCakeOfTheWeek;
                selectedCake.Name = cake.Name;

                _context.Cakes.Update(selectedCake);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Cake doesnt exists");
            }
        }

        public async Task<int> DeleteCakeAsync(int id)
        {
            var selectedCake =await _context.Cakes.FirstOrDefaultAsync(c => c.CakeId==id);
            if (selectedCake != null) 
            {
                _context.Cakes.Remove(selectedCake);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The cake to delete can't be found.");
            }
        }

        public async Task<int> GetAllCakesCountAsync()
        {
            var count = await _context.Cakes.CountAsync();
            return count;
        }

        public async Task<IEnumerable<Cake>> GetCakesPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Cake> cakes = from c in _context.Cakes
                                     select c;
            pageNumber ??= 1;
            cakes = cakes.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);
            return await cakes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Cake>> GetCakesSortedAndPagedAsync(string sortBy, int? pageNumber, int pageSize)
        {
            IQueryable<Cake> cakes = from c in _context.Cakes
                                     select c;
            switch(sortBy)
            {
                case "name_desc":
                    cakes = cakes.OrderByDescending(c => c.Name); break;
                case "name":
                    cakes = cakes.OrderBy(c => c.Name); break;
                case "id_desc":
                    cakes = cakes.OrderByDescending(c => c.CakeId); break;
                case "id":
                    cakes = cakes.OrderBy(c => c.CakeId); break;
                case "price_desc":
                    cakes = cakes.OrderByDescending(c => c.Price); break;
                case "price":
                    cakes = cakes.OrderBy(c => c.Price); break;
            }

            pageNumber ??= 1;

            cakes = cakes.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);
            return await cakes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Cake>> SearchCakes(string searchQuery, int? categoryId)
        {
            IQueryable<Cake> cakes = from c in _context.Cakes
                                     select c;

            if(!string.IsNullOrEmpty(searchQuery))
            {
                cakes = cakes.Where(c => c.Name.Contains(searchQuery) || c.ShortDescription.Contains(searchQuery) || c.LongDescription.Contains(searchQuery));
            }

            if(categoryId != null)
            {
                cakes = _context.Cakes.Where(c => c.CategoryId == categoryId);
            }

            return await cakes.ToListAsync();
        }
    }
}

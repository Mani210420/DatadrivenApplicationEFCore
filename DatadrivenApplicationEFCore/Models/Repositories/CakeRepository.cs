
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
    }
}

using Newtonsoft.Json.Linq;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAllCakesAsync();
        Task<Cake?> GetCakeByIdAsync(int id);
        Task<int> AddCakeAsync(Cake cake);
        Task<int> UpdateCakeAsync(Cake cake);
        Task<int> DeleteCakeAsync(int id);
        Task<int> GetAllCakesCountAsync();
        Task<IEnumerable<Cake>> GetCakesPagedAsync(int? pageNumber, int pageSize);
        Task<IEnumerable<Cake>> GetCakesSortedAndPagedAsync(string sortBy, int? pageNumber, int pageSize);
        Task<IEnumerable<Cake>> SearchCakes(string searchQuery, int? categoryId);
    }
}

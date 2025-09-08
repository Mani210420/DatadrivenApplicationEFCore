using Newtonsoft.Json.Linq;

namespace DatadrivenApplicationEFCore.Models.Repositories
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAllCakesAsync();
        Task<Cake?> GetCakeByIdAsync(int id);
        Task<int> AddCakeAsync(Cake cake);
        Task<int> UpdateCakeAsync(Cake cake);
    }
}

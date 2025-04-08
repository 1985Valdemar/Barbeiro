using BarberTech.Models;

namespace BarberTech.Repositories.Barbeiros
{
    public interface IBarbeiroRepository
    {
        Task AddAsync (Barbeiro barbeiro);
        Task UpdateAsync(Barbeiro barbeiro);
        Task<List<Barbeiro>> GetAllAsync ();
        Task<Barbeiro?> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}

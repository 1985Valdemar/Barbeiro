using BarberTech.Models;

namespace BarberTech.Repositories.Clientes
{
    public interface IClienteRepository
    {
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task<List<Cliente>> GetAllAsync();
        Task DeleteByIdAsync(int id);
        Task<Cliente?> GetByIdAsync(int id);
    }
}

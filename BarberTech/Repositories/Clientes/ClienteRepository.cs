using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberTech.Repositories.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var cliente = await GetByIdAsync(id);
            _context.Clientes.Remove(cliente!);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return  await _context
                .Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context
                .Clientes
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }
}

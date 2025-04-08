using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberTech.Repositories.Barbeiros
{
    public class BarbeiroRepository : IBarbeiroRepository
    {
        private readonly ApplicationDbContext _context;

        public BarbeiroRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Barbeiro barbeiro)
        {
            try
            {
                _context.Barbeiros.Add(barbeiro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar barbeiro", ex);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var barbeiro = await GetByIdAsync(id);
            _context.Barbeiros.Remove(barbeiro!);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Barbeiro>> GetAllAsync()
        {
            return await _context.Barbeiros
                .Include(e => e.Especialidade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Barbeiro?> GetByIdAsync(int id)
        {
            return await _context.Barbeiros.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Barbeiro barbeiro)
        {
            try
            {
                _context.Update(barbeiro);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _context.ChangeTracker.Clear();
                throw;

            }
        }
    }
}

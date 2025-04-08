using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                if (barbeiro == null)
                    throw new ArgumentNullException(nameof(barbeiro));

                // Converter datas para UTC (exemplo com propriedade hipotética "DataCadastro")
                if (barbeiro.DataCadastro.Kind == DateTimeKind.Local)
                {
                    barbeiro.DataCadastro = barbeiro.DataCadastro.ToUniversalTime();
                }

                _context.Barbeiros.Add(barbeiro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO SALVAR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao cadastrar barbeiro. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var barbeiro = await _context.Barbeiros
                    .FirstOrDefaultAsync(b => b.Id == id)
                    ?? throw new KeyNotFoundException($"Barbeiro com ID {id} não encontrado");

                _context.Barbeiros.Remove(barbeiro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO EXCLUIR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao excluir barbeiro. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<List<Barbeiro>> GetAllAsync()
        {
            try
            {
                return await _context.Barbeiros
                    .Include(b => b.Especialidade)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO AO LISTAR: {ex.Message}");
                throw;
            }
        }

        public async Task<Barbeiro?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Barbeiros
                    .Include(b => b.Especialidade)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO AO BUSCAR: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(Barbeiro barbeiro)
        {
            try
            {
                var existing = await _context.Barbeiros.FindAsync(barbeiro.Id)
                    ?? throw new KeyNotFoundException($"Barbeiro com ID {barbeiro.Id} não encontrado");

                // Converter datas para UTC (exemplo com propriedade hipotética "DataCadastro")
                if (barbeiro.DataCadastro.Kind == DateTimeKind.Local)
                {
                    barbeiro.DataCadastro = barbeiro.DataCadastro.ToUniversalTime();
                }

                // Atualizar apenas campos permitidos
                _context.Entry(existing).CurrentValues.SetValues(barbeiro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO ATUALIZAR: {ex.InnerException?.Message}");
                _context.ChangeTracker.Clear();
                throw new ApplicationException("Erro ao atualizar barbeiro. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }
    }
}

using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                if (cliente == null)
                    throw new ArgumentNullException(nameof(cliente));

                if (string.IsNullOrWhiteSpace(cliente.Nome))
                    throw new ArgumentException("Nome do cliente é obrigatório");

                if (cliente.Nascimento.Kind == DateTimeKind.Local)
                {
                    cliente.Nascimento = cliente.Nascimento.ToUniversalTime();
                }

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao salvar cliente. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                // Consulta direta sem AsNoTracking
                var cliente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                    throw new KeyNotFoundException($"Cliente com ID {id} não encontrado");

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO EXCLUIR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao excluir cliente. Detalhes: " + ex.InnerException?.Message, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO INESPERADO: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            try
            {
                var existingCliente = await _context.Clientes.FindAsync(cliente.Id)
                    ?? throw new KeyNotFoundException($"Cliente com ID {cliente.Id} não encontrado");

                if (cliente.Nascimento.Kind == DateTimeKind.Local)
                {
                    cliente.Nascimento = cliente.Nascimento.ToUniversalTime();
                }

                _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO NA ATUALIZAÇÃO: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao atualizar cliente. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }
    }
}

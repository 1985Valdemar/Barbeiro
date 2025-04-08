using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberTech.Repositories.Agendamentos
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Agendamento agendamento)
        {
            try
            {
                if (agendamento == null)
                    throw new ArgumentNullException(nameof(agendamento));

                // Converter Data para UTC
                if (agendamento.Data.Kind == DateTimeKind.Local)
                {
                    agendamento.Data = agendamento.Data.ToUniversalTime();
                }

                // Validar relacionamentos
                if (!await _context.Clientes.AnyAsync(c => c.Id == agendamento.ClienteId))
                    throw new ArgumentException("Cliente não encontrado");

                if (!await _context.Barbeiros.AnyAsync(b => b.Id == agendamento.BarbeiroId))
                    throw new ArgumentException("Barbeiro não encontrado");

                _context.Agendamentos.Add(agendamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO SALVAR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao salvar agendamento. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                // Buscar com tracking para exclusão
                var agendamento = await _context.Agendamentos
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (agendamento == null)
                    throw new KeyNotFoundException($"Agendamento com ID {id} não encontrado");

                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO EXCLUIR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao excluir agendamento. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<List<Agendamento>> GetAllAsync()
        {
            try
            {
                return await _context.Agendamentos
                    .Include(a => a.Cliente)
                    .Include(a => a.Barbeiro)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO AO LISTAR: {ex.Message}");
                throw;
            }
        }

        public async Task<Agendamento?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Agendamentos
                    .Include(a => a.Cliente)
                    .Include(a => a.Barbeiro)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO AO BUSCAR: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(Agendamento agendamento)
        {
            try
            {
                var existing = await _context.Agendamentos.FindAsync(agendamento.Id)
                    ?? throw new KeyNotFoundException($"Agendamento com ID {agendamento.Id} não encontrado");

                // Converter Data para UTC
                if (agendamento.Data.Kind == DateTimeKind.Local)
                {
                    agendamento.Data = agendamento.Data.ToUniversalTime();
                }

                // Atualizar apenas campos permitidos
                _context.Entry(existing).CurrentValues.SetValues(new
                {
                    agendamento.Data,
                    agendamento.ClienteId,
                    agendamento.BarbeiroId
                });

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERRO AO ATUALIZAR: {ex.InnerException?.Message}");
                throw new ApplicationException("Erro ao atualizar agendamento. Detalhes: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<List<AgendamentosAnuais>?> GetReportAsync()
        {
            try
            {
                var currentYear = DateTime.UtcNow.Year;
                var query = $"""
                    SELECT 
                        EXTRACT(MONTH FROM "Data" AT TIME ZONE 'UTC') AS Mes,
                        COUNT(*) AS Quantidade 
                    FROM "Agendamentos" 
                    WHERE EXTRACT(YEAR FROM "Data" AT TIME ZONE 'UTC') = {currentYear}
                    GROUP BY Mes 
                    ORDER BY Mes;
                    """;

                return await _context.Database.SqlQueryRaw<AgendamentosAnuais>(query)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO NO RELATÓRIO: {ex.Message}");
                throw;
            }
        }
    }
}

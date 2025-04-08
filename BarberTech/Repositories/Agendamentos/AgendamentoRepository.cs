using BarberTech.Data;
using BarberTech.Models;
using Microsoft.EntityFrameworkCore;

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
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var agendamentos = await GetByIdAsync(id);
            _context.Agendamentos.Remove(agendamentos!);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Agendamento>> GetAllAsync()
        {
            return await _context
                .Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Barbeiro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Agendamento?> GetByIdAsync(int id)
        {
            return await _context
                .Agendamentos
                .Include(x => x.Cliente)
                .Include(x => x.Barbeiro)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AgendamentosAnuais>?> GetReportAsync()
        {
            var result = _context.Database.SqlQuery<AgendamentosAnuais>
                ($"SELECT MONTH(Data) AS Mes, COUNT(*) AS Quantidade FROM Agendamentos WHERE YEAR(Data) = {DateTime.Today.Year} GROUP BY MONTH(Data) ORDER BY Mes;");

            return await Task.FromResult(result.ToList());
        }
    }
}

using BarberTech.Models;

namespace BarberTech.Repositories.Agendamentos
{
    public interface IAgendamentoRepository
    {
        Task<List<Agendamento>> GetAllAsync();
        Task AddAsync(Agendamento agendamento);
        Task DeleteByIdAsync(int id);
        Task<Agendamento?> GetByIdAsync(int id);
        Task<List<AgendamentosAnuais>?> GetReportAsync();
    }
}

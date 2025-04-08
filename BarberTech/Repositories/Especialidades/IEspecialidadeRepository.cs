using BarberTech.Models;

namespace BarberTech.Repositories.Especialidades
{
    public interface IEspecialidadeRepository
    {
        Task<List<Especialidade>> GetAllAsync();
    }
}

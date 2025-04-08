namespace BarberTech.Models
{
    public class Barbeiro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; } = null!;
        public DateTime DataCadastro { get; set; }

        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}

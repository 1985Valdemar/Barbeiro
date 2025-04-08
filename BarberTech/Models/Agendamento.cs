namespace BarberTech.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public int BarbeiroId { get; set; }
        public Barbeiro Barbeiro { get; set; } = null!;
        public TimeSpan Horario { get; set; }
        public DateTime Data { get; set; }
    }
}

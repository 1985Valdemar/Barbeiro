namespace BarberTech.Models
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public List<Barbeiro> Barbeiros { get; set; } = new List<Barbeiro>();
    }
}

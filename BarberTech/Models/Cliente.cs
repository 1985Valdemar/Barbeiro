namespace BarberTech.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get;  set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateTime Nascimento { get; set; }


        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    }
}

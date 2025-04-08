using System.ComponentModel.DataAnnotations;

namespace BarberTech.Components.Pages.Clientes
{
    public class ClienteInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome deve ser fornecido")]
        [MaxLength(50, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Documento deve ser fornecido")]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "E-mail deve ser fornecido")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(50, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Telefone deve ser fornecido")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "Data de nascimento deve ser fornecida")]
        public DateTime Nascimento { get; set; } = DateTime.Today;
    }
}


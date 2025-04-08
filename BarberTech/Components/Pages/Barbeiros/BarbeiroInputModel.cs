using System.ComponentModel.DataAnnotations;

namespace BarberTech.Components.Pages.Barbeiros
{
    public class BarbeiroInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{O} é obrigatório")]
        [MaxLength(50, ErrorMessage = "{0} deve conter no máximo {1} caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "{0} deve ser fornecido")]
        public string Documento { get; set; } = null!;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "{0} deve ser fornecido")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "E-mail deve ser fornecido")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(50, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "{0} deve ser fornecido")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Valor selecionado é inválido")]
        public int EspecialidadeId { get; set; }
    }
}

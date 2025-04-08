using System.ComponentModel.DataAnnotations;

namespace BarberTech.Components.Pages.Agendamentos
{
    public class AgendamentoInputModel
    {
        
            [MaxLength(250, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
            public string? Descricao { get; set; }

            [Required(ErrorMessage = "{0} deve ser fornecido")]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Valor selecionado é inválido")]
            public int ClienteId { get; set; }

            [Required(ErrorMessage = "{0} deve ser fornecido")]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Valor selecionado é inválido")]
            public int BarbeiroId { get; set; }

            [Required(ErrorMessage = "{0} deve ser fornecido")]
            public TimeSpan Hora { get; set; }

            [Required(ErrorMessage = "{0} deve ser fornecido")]
            public DateTime Data { get; set; }
        
    }
}

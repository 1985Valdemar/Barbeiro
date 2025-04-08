using BarberTech.Models;
using BarberTech.Repositories.Agendamentos;
using BarberTech.Repositories.Barbeiros;
using BarberTech.Repositories.Clientes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BarberTech.Components.Pages.Agendamentos
{
    public class CreateAgendamentoPage : ComponentBase
    {
        [Inject]
        private IAgendamentoRepository AgendamentoRepository { get; set; } = null!;

        [Inject]
        private IBarbeiroRepository BarbeiroRepository { get; set; } = null!;

        [Inject]
        private IClienteRepository ClienteRepository { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = null!;

        public AgendamentoInputModel InputModel { get; set; } = new();

        public List<Barbeiro> Barbeiros { get; set; } = new();

        public List<Cliente> Clientes { get; set; } = new();

        public TimeSpan? Time = new TimeSpan(09, 00, 00);
        public DateTime? Date { get; set; } = DateTime.Now.Date;
        public DateTime? MinDate { get; set; } = DateTime.Now.Date;

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext.Model is AgendamentoInputModel model)
                {
                    var agendamento = new Agendamento
                    {
                        Descricao = model.Descricao,
                        ClienteId = model.ClienteId,
                        BarbeiroId = model.BarbeiroId,
                        Horario = Time!.Value,
                        Data = Date!.Value
                    };
                    await AgendamentoRepository.AddAsync(agendamento);

                    Snackbar.Add("Agendamento criado com sucesso!", Severity.Success);
                    NavigationManager.NavigateTo("/agendamentos");

                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }

        }
        protected override async Task OnInitializedAsync()
        {
            Barbeiros = await BarbeiroRepository.GetAllAsync();
            Clientes = await ClienteRepository.GetAllAsync();
        }
    }
}

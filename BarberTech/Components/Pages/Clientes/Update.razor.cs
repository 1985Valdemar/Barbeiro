using BarberTech.Extensions;
using BarberTech.Models;
using BarberTech.Repositories.Clientes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BarberTech.Components.Pages.Clientes
{
    public class UpdateCliente : ComponentBase
    {
        [Parameter]
        public int ClienteId { get; set; }

        [Inject]
        public IClienteRepository ClienteRepository { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public ClienteInputModel InputModel { get; set; } = new();

        private Cliente? CurrentCliente { get; set; }

        public DateTime? Nascimento { get; set; } = DateTime.Today;

        public DateTime? MaxDate { get; set; } = DateTime.Today;

        protected override async Task OnInitializedAsync()
        {
            CurrentCliente = await ClienteRepository.GetByIdAsync(ClienteId);

            if (CurrentCliente is null)
                return;

            InputModel = new ClienteInputModel
            {
                Id = CurrentCliente.Id,
                Nome = CurrentCliente.Nome,
                Documento = CurrentCliente.Documento,
                Email = CurrentCliente.Email,
                Telefone = CurrentCliente.Telefone,
                Nascimento = CurrentCliente.Nascimento
            };

            Nascimento = CurrentCliente.Nascimento;
        }

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext.Model is ClienteInputModel model)
                {
                    CurrentCliente!.Nome = model.Nome;
                    CurrentCliente.Documento = model.Documento.SomenteCaracteres();
                    CurrentCliente.Email = model.Email;
                    CurrentCliente.Telefone = model.Telefone.SomenteCaracteres();
                    CurrentCliente.Nascimento = model.Nascimento;

                    await ClienteRepository.UpdateAsync(CurrentCliente);

                    Snackbar.Add($"Cliente {CurrentCliente.Nome} atualizado com sucesso!", Severity.Success);
                    NavigationManager.NavigateTo("/clientes");
                }

            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}

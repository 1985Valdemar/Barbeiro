using BarberTech.Models;
using BarberTech.Repositories.Clientes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace BarberTech.Components.Pages.Clientes
{
    public class IndexPage : ComponentBase
    {
        [Inject]
        public IClienteRepository ClienteRepository { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationState { get; set; }

        public bool HideButtons { get; set; }

        public async Task DeleteCliente(Cliente cliente)
        {
            try
            {
                var result = await DialogService.ShowMessageBox
                    (
                        "Atenção",
                        $"Deseja realmente excluir o cliente {cliente.Nome}?",
                        yesText: "SIM",
                        cancelText: "NÃO"
                    );
                if (result is true)
                {
                    await ClienteRepository.DeleteByIdAsync(cliente.Id);
                    Snackbar.Add($"Cliente {cliente.Nome} excluído com sucesso", Severity.Success);
                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }

        }


        public void GoToUpdate(int id)
        {
            NavigationManager.NavigateTo($"/clientes/update/{id}");
        }

        protected override async Task OnInitializedAsync()
        {
            var auth = await AuthenticationState!;

            HideButtons = !auth.User.IsInRole("Atendente");

            Clientes = await ClienteRepository.GetAllAsync();
        }
    }
}

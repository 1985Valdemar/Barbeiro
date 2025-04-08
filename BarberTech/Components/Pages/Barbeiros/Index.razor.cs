using BarberTech.Models;
using BarberTech.Repositories.Barbeiros;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace BarberTech.Components.Pages.Barbeiros
{
    public class IndexBarbeiroPage : ComponentBase  
    {
        [Inject]
        public IBarbeiroRepository Repository { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        public List<Barbeiro> Barbeiros { get; set; } = new();

        public async Task DaleteBarbeiroAsync(Barbeiro barbeiro)
        {
            try
            {
                var result = await DialogService.ShowMessageBox
                    (
                        "Atenção",
                        $"Deseja realmente excluir o profissional {barbeiro.Nome}?",
                        yesText: "SIM",
                        cancelText: "NÃO"
                    );
                if (result is true)
                {
                    await Repository.DeleteByIdAsync(barbeiro.Id);
                    Snackbar.Add($"Profissional {barbeiro.Nome} excluído com sucesso", Severity.Success);
                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }

        }

        public bool HideButtons { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationState { get; set; }


        public void GoToUpdate(int id)
            => Navigation.NavigateTo($"/barbeiros/update/{id}");
        

        protected override async Task OnInitializedAsync()
        {
            var auth = await AuthenticationState!;

            HideButtons = !auth.User.IsInRole("Atendente");

            Barbeiros = await Repository.GetAllAsync();
        }


    }
}

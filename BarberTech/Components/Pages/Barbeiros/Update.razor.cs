using BarberTech.Extensions;
using BarberTech.Models;
using BarberTech.Repositories.Barbeiros;
using BarberTech.Repositories.Especialidades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BarberTech.Components.Pages.Barbeiros
{
    public class UpdateBerbeiroPage : ComponentBase
    {
        [Parameter]
        public int BarbeiroId { get; set; }

        [Inject]
        private IEspecialidadeRepository EspecialidadeRepository { get; set; } = null!;

        [Inject]
        public IBarbeiroRepository Repository { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public Barbeiro? CurrentBarbeiro { get; set; }

        public BarbeiroInputModel InputModel { get; set; } = new();

        public List<Especialidade> Especialidades { get; set; } = new();

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (CurrentBarbeiro is null) return;

                if(editContext.Model is BarbeiroInputModel model)
                {
                    CurrentBarbeiro.Nome = model.Nome;
                    CurrentBarbeiro.Documento = model.Documento.SomenteCaracteres();
                    CurrentBarbeiro.Email = model.Email;
                    CurrentBarbeiro.Telefone = model.Telefone.SomenteCaracteres();
                    CurrentBarbeiro.EspecialidadeId = model.EspecialidadeId;

                    await Repository.UpdateAsync(CurrentBarbeiro);
                    Snackbar.Add("Barbeiro atualizado com sucesso", Severity.Success);
                    NavigationManager.NavigateTo("/barbeiros");
                }

            }

            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            CurrentBarbeiro = await Repository.GetByIdAsync(BarbeiroId);
            Especialidades = await EspecialidadeRepository.GetAllAsync();

            if (CurrentBarbeiro is null) return;

            InputModel = new BarbeiroInputModel
            {
                Nome = CurrentBarbeiro.Nome,
                Documento = CurrentBarbeiro.Documento,
                Email = CurrentBarbeiro.Email,
                Telefone = CurrentBarbeiro.Telefone,
                EspecialidadeId = CurrentBarbeiro.EspecialidadeId
            };

        }
    }
}

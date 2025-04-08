using BarberTech.Extensions;
using BarberTech.Models;
using BarberTech.Repositories.Barbeiros;
using BarberTech.Repositories.Especialidades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BarberTech.Components.Pages.Barbeiros
{
    public class CreateBarbeiroPage : ComponentBase
    {
        [Inject]
        private IEspecialidadeRepository EspecialidadeRepository { get; set; } = null!;

        [Inject]
        public IBarbeiroRepository Repository { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public List<Especialidade> Especialidades { get; set; } = new();

        public BarbeiroInputModel InputModel { get; set; } = new();

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext.Model is BarbeiroInputModel model)
                {
                    var barbeiro = new Barbeiro
                    {
                        Nome = model.Nome,
                        Documento = model.Documento.SomenteCaracteres(),
                        Telefone = model.Telefone.SomenteCaracteres(),
                        Email = model.Email,
                        EspecialidadeId = model.EspecialidadeId,
                        DataCadastro = model.DataCadastro
                    };

                    await Repository.AddAsync(barbeiro);
                    Snackbar.Add("Profissional cadastrado com sucesso!", Severity.Success);
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
            Especialidades = await EspecialidadeRepository.GetAllAsync();
        }




    }
}

using BarberTech.Extensions;
using BarberTech.Models;
using BarberTech.Repositories.Clientes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BarberTech.Components.Pages.Clientes
{
    public class CreateClientePage : ComponentBase
    {
        [Inject]
        public IClienteRepository ClienteRepository { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        public ClienteInputModel ClienteInputModel { get; set; } = new();

        public DateTime? Nascimento { get; set; } = DateTime.Today;

        public DateTime? MaxDate { get; set; } = DateTime.Today;

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext.Model is ClienteInputModel model)
                {
                    var cliente = new Cliente
                    {
                        Nome = model.Nome,
                        Documento = model.Documento.SomenteCaracteres(),
                        Email = model.Email,
                        Telefone = model.Telefone.SomenteCaracteres(),
                        Nascimento = Nascimento!.Value
                    };

                    await ClienteRepository.AddAsync(cliente);
                    Snackbar.Add("Cliente cadastrado com sucesso", Severity.Success);
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

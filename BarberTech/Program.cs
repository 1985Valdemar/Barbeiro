using BarberTech.Components;
using BarberTech.Components.Account;
using BarberTech.Data;
using BarberTech.Repositories.Agendamentos;
using BarberTech.Repositories.Barbeiros;
using BarberTech.Repositories.Clientes;
using BarberTech.Repositories.Especialidades;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Razor Components and Authentication
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Injeção de dependência dos repositórios
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();

// Configurar Identity com roles e tokens
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager()
    .AddRoleManager<RoleManager<IdentityRole>>();

// Configurar envio de email (mock)
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Configurar o banco de dados (PostgreSQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Mapear rotas padrão do Identity (inclui /Account/AccessDenied)
app.MapAdditionalIdentityEndpoints();

app.Run();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Criar usuário Valdemar
    var userValdemar = await userManager.FindByNameAsync("valdemar");
    if (userValdemar == null)
    {
        var novoUser = new ApplicationUser
        {
            UserName = "valdemar",
            Email = "valdemar@teste.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(novoUser, "SenhaForte@123");
    }

    // Criar usuário Daniela
    var userDaniela = await userManager.FindByNameAsync("daniela");
    if (userDaniela == null)
    {
        var novoUser = new ApplicationUser
        {
            UserName = "daniela",
            Email = "daniela@teste.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(novoUser, "SenhaForte@123");
    }
}

app.Run();

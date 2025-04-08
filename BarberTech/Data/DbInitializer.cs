using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BarberTech.Models;

namespace BarberTech.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        internal void Seed()
        {
            _builder.Entity<IdentityRole>().HasData
             (
                 new IdentityRole
                 {
                     Id = "293654e0-decb-4c1a-bfe9-d8e069964124",
                     Name = "Atendente",
                     NormalizedName = "ATENDENTE"
                 },

                 new IdentityRole
                 {
                     Id = "d209e75d-3ea1-4910-b7a9-8f6f56e23e2e",
                     Name = "Barbeiro",
                     NormalizedName = "BARBEIRO"
                 }
             );

            var hasher = new PasswordHasher<IdentityUser>();
            var securityStamp = "62584b4a-5929-452e-bf58-a251ca73e3be";
            var concurrencyStamp = "5d4e6257-c5f9-4c39-8404-525c8e864059";

            _builder.Entity<Atendente>().HasData
            (
                new Atendente
                {
                    Id = "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                    Nome = "Barber Tech",
                    Email = "barbertech@hotmail.com.br",
                    EmailConfirmed = true,
                    UserName = "barbertech@hotmail.com.br",
                    NormalizedEmail = "BARBERTECH@HOTMAIL.COM.BR",
                    NormalizedUserName = "BARBERTECH@HOTMAIL.COM.BR",
                    PasswordHash = "AQAAAAEAACcQAAAAECDC1zQxmtos3I/iwDWxHN3rdg0G0BMgbJuWm8dhLdzezzSjWo2QzqelOcuK73h9eg==",
                    SecurityStamp = securityStamp,
                    ConcurrencyStamp = concurrencyStamp
                }
            );

            _builder.Entity<IdentityUserRole<string>>().HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "293654e0-decb-4c1a-bfe9-d8e069964124",
                    UserId = "e2d55110-6114-4f30-8c78-e07eb6861a8b"
                }
            );

            _builder.Entity<Especialidade>().HasData
            (
                new Especialidade
                {
                    Id = 1,
                    Nome = "Corte",
                    Descricao = "Corte de cabelo"
                },
                new Especialidade
                {
                    Id = 2,
                    Nome = "Barba",
                    Descricao = "Talento na barba"
                }
            );
        }
    }
}

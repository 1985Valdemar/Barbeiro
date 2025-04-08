using BarberTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberTech.Data.Configurations
{
    public class BarbeiroConfig : IEntityTypeConfiguration<Barbeiro>
    {
        public void Configure(EntityTypeBuilder<Barbeiro> builder)
        {
            builder.ToTable("Barbeiros");

            builder.HasKey(b => b.Id);


            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Documento)
                .IsRequired(true)
                .HasColumnType("VARCHAR(11)");

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Telefone)
                .IsRequired(true)
                .HasColumnType("VARCHAR(11)");

            builder.Property(x => x.EspecialidadeId)
                .IsRequired(true);

            builder.HasIndex(x => x.Documento)
                .IsUnique();

            builder.HasMany(a => a.Agendamentos)
                .WithOne(b => b.Barbeiro)
                .HasForeignKey(a => a.BarbeiroId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

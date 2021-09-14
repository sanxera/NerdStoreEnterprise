using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Core.DomainObjects;

namespace NSE.Client.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Models.Client>
    {
        public void Configure(EntityTypeBuilder<Models.Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.AddressEmail)
                    .IsRequired()
                    .HasColumnName("AddressEmail")
                    .HasColumnType($"varchar({Email.EmailMaxLength})");
            });

            builder.HasOne(c => c.Address)
                .WithOne(c => c.Client);

            builder.ToTable("Clients");
        }
    }
}
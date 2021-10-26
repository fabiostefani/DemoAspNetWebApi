using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings;
public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("Nome");

        builder.Property(x => x.Descricao)
            .HasMaxLength(1000)
            .IsRequired()
            .HasColumnName("Descricao");

        builder.Property(x => x.Imagem)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnName("Imagem");

        builder.Property(x => x.Ativo)
            .HasColumnName("Ativo")
            .IsRequired();

        builder.ToTable("Produtos");
    }
}
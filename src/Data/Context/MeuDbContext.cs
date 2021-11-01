using Microsoft.EntityFrameworkCore;

using Business.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context;
public class MeuDbContext : DbContext
{
    public MeuDbContext(DbContextOptions<MeuDbContext> options)
        : base(options)
    {

    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

        //TRATAMENTO PARA NÃO PERMITIR EXCLUSÕES COM DELETE CASCADE
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        base.OnModelCreating(modelBuilder);
    }
}

public class ProductsDbContextFactory : IDesignTimeDbContextFactory<MeuDbContext>
{
    public MeuDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MeuDbContext>();
                optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Pooling = true; Database = DemoAspNetWebApi; User Id = postgres; Password = Postgres2021!");

        return new MeuDbContext(optionsBuilder.Options);
    }
}     
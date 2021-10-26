using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;
public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
{
    public EnderecoRepository(MeuDbContext context) : base(context) { }

    public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
    {
        return await Db.Enderecos.AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
    }
}
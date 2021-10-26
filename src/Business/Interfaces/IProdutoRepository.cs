using Business.Models;

namespace Business.Interfaces;
public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid id);
    Task<IEnumerable<Produto>> ObterProdutosFornecedores();
    Task<Produto> ObterProdutoFornecedor(Guid id);
}
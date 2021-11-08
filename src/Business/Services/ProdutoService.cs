using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services;
public class ProdutoService : BaseService, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    public IUser _User { get; }

    public ProdutoService(IProdutoRepository produtoRepository,
                          INotificador notificador,
                          IUser user) : base(notificador)
    {
        _User = user;
        _produtoRepository = produtoRepository;
    }

    public async Task Adicionar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

        var user = _User.GetUserId();

        await _produtoRepository.Adicionar(produto);
    }

    public async Task Atualizar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

        await _produtoRepository.Atualizar(produto);
    }

    public async Task Remover(Guid id)
    {
        await _produtoRepository.Remover(id);
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }
}
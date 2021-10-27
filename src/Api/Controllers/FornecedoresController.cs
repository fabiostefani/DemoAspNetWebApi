using System.Collections;
using Api.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class FornecedoresController : MainController
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IFornecedorService _fornecedorService;
    private readonly IMapper _mapper;
    public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                  IFornecedorService fornecedorService,
                                  IMapper mapper)
    {
        _fornecedorRepository = fornecedorRepository;
        _fornecedorService = fornecedorService;
        _mapper = mapper;

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
    {
        var fornecedores =_mapper.Map<IEnumerable<FornecedorViewModel>>( await _fornecedorRepository.ObterTodos());
        return Ok(fornecedores);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
    {
        var fornecedor = await ObterFornecedorProdutosEndereco(id);
        if (fornecedor == null) return NotFound();
        return fornecedor;
    }

    // [ClaimsAuthorize("Fornecedor","Adicionar")]
    [HttpPost]
    public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
        var sucesso = await _fornecedorService.Adicionar(fornecedor);
        if (!sucesso) return BadRequest();        
        return Ok(fornecedorViewModel);
    }

    // [ClaimsAuthorize("Fornecedor", "Atualizar")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
    {
        if (id != fornecedorViewModel.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest();
        var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
        var sucesso = await _fornecedorService.Atualizar(fornecedor);
        if (!sucesso) return BadRequest();
        return Ok(fornecedor);
    }

    // [ClaimsAuthorize("Fornecedor", "Excluir")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
    {
        var fornecedorViewModel = await ObterFornecedorEndereco(id);
        if (fornecedorViewModel == null) return NotFound();
        var sucesso = await _fornecedorService.Remover(id);
        if (!sucesso) return BadRequest();        
        return Ok(fornecedorViewModel);
    }

    // [HttpGet("endereco/{id:guid}")]
    // public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
    // {
    //     return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
    // }

    // [ClaimsAuthorize("Fornecedor", "Atualizar")]
    // [HttpPut("endereco/{id:guid}")]
    // public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
    // {
    //     if (id != enderecoViewModel.Id)
    //     {
    //         NotificarErro("O id informado não é o mesmo que foi passado na query");
    //         return CustomResponse(enderecoViewModel);
    //     }

    //     if (!ModelState.IsValid) return CustomResponse(ModelState);

    //     await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));

    //     return CustomResponse(enderecoViewModel);
    // }

    private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
    {
        return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
    }

    private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
    {
        return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
    }
}
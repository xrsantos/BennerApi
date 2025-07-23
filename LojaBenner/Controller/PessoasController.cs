using System.Runtime.InteropServices;
using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaBenner.Controller;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _PessoaService;
    private readonly IPedidoService _pedidoService;

    public PessoasController(IPessoaService Pessoaservice, IPedidoService pedidoService)
    {
        _PessoaService = Pessoaservice ?? throw new ArgumentNullException(nameof(Pessoaservice));
        _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _PessoaService.GetAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _PessoaService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(PessoaCreateDto PessoaCreateDto)
    {
        var Pessoa = new Pessoa { Nome = PessoaCreateDto.Nome, Endereco = PessoaCreateDto.Endereco, Cpf = PessoaCreateDto.Cpf };
        await _PessoaService.CreateAsync(Pessoa);
        return Ok(Pessoa);
    }

    [HttpPut]
    public async Task<IActionResult> Update(PessoaCreateDto PessoaCreateDto)
    {
        var Pessoa = new Pessoa { Nome = PessoaCreateDto.Nome, Endereco = PessoaCreateDto.Endereco, Id = PessoaCreateDto.id, Cpf = PessoaCreateDto.Cpf };
        await _PessoaService.UpdateAsync(Pessoa);
        return Ok(Pessoa);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _PessoaService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    [Route("${pessoaId}/pedidos")]
    public async Task<IActionResult> GetPedidos(int pessoaId)
    {
        List<PedidoResumoDto> pedidos = await _pedidoService.GetByPessoaId(pessoaId);

        return Ok(pedidos);
    }


}

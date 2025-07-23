using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LojaBenner.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _ProdutoService;

    public ProdutosController(IProdutoService Produtoservice)
    {
        _ProdutoService = Produtoservice ?? throw new ArgumentNullException(nameof(Produtoservice));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _ProdutoService.GetAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _ProdutoService.GetAllAsync());
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProdutoCreateDto ProdutoCreateDto)
    {
        var produto = new Produto { Nome = ProdutoCreateDto.Nome, Codigo = ProdutoCreateDto.Codigo, Valor = ProdutoCreateDto.Valor };
        await _ProdutoService.CreateAsync(produto);
        return Ok(produto);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(ProdutoCreateDto produtoCreateDto)
    {
        var produto = new Produto { Id= produtoCreateDto.Id, Nome = produtoCreateDto.Nome, Codigo = produtoCreateDto.Codigo, Valor = produtoCreateDto.Valor };
        await _ProdutoService.UpdateAsync(produto);
        return Ok(produto);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ProdutoService.DeleteAsync(id);
        return Ok();
    }

}

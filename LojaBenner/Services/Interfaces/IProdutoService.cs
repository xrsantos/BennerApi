using LojaBenner.Dtos;
using LojaBenner.Entities;

namespace LojaBenner.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto?> CreateAsync(Produto Produto);
        Task DeleteAsync(int id);
        Task<IReadOnlyList<Produto?>> GetAllAsync();
        Task<Produto?> GetAsync(int id);
        Task<IReadOnlyList<Produto?>> GetFilterAsync(ProdutoFilterDto produtoFilterDto);
        Task<Produto?> UpdateAsync(Produto Produto);
    }
}
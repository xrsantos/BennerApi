using LojaBenner.Entities;

namespace LojaBenner.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa?> CreateAsync(Pessoa Pessoa);
        Task DeleteAsync(int id);
        Task<IReadOnlyList<Pessoa?>> GetAllAsync();
        Task<Pessoa?> GetAsync(int id);
        Task<Pessoa?> UpdateAsync(Pessoa Pessoa);
    }
}
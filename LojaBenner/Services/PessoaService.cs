using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Infrastructure.Repositories;
using LojaBenner.Infrastructure.UnitOfWork;
using LojaBenner.Infrastructure.UnitOfWork.Interfaces;
using LojaBenner.Services.Interfaces;

namespace LojaBenner.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PessoaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<IReadOnlyList<Pessoa?>> GetAllAsync()
        {
            return await _unitOfWork.Pessoas.SearchAsync();
        }
        public async Task<Pessoa?> GetAsync(int id)
        {
            return await _unitOfWork.Pessoas.GetByIdAsync(id);
        }
        public async Task<Pessoa?> CreateAsync(Pessoa Pessoa)
        {
            await _unitOfWork.Pessoas.Create(Pessoa);
            await _unitOfWork.SaveChangesAsync();

            return Pessoa;
        }
        public async Task<Pessoa?> UpdateAsync(Pessoa Pessoa)
        {
            _unitOfWork.Pessoas.Edit(Pessoa);
            await _unitOfWork.SaveChangesAsync();

            return Pessoa;
        }
        public async Task DeleteAsync(int id)
        {
            var Pessoa = await _unitOfWork.Pessoas.GetByIdAsync(id);
            if (Pessoa == null)
            {
                return;
            }

            _unitOfWork.Pessoas.Delete(Pessoa);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Infrastructure.UnitOfWork.Interfaces;
using LojaBenner.Services.Interfaces;

namespace LojaBenner.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProdutoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<IReadOnlyList<Produto?>> GetAllAsync()
        {
            return await _unitOfWork.Produtos.SearchAsync(null, null, null, null);
        }
        public async Task<IReadOnlyList<Produto?>> GetFilterAsync(ProdutoFilterDto produtoFilterDto)
        {
            return await _unitOfWork.Produtos.SearchAsync(produtoFilterDto.Codigo, produtoFilterDto.Nome, produtoFilterDto.ValorMin, produtoFilterDto.ValorMax);
        }
        public async Task<Produto?> GetAsync(int id)
        {
            return await _unitOfWork.Produtos.GetByIdAsync(id);
        }
        public async Task<Produto?> CreateAsync(Produto Produto)
        {
            await _unitOfWork.Produtos.Create(Produto);
            await _unitOfWork.SaveChangesAsync();

            return Produto;
        }
        public async Task<Produto?> UpdateAsync(Produto Produto)
        {
            _unitOfWork.Produtos.Edit(Produto);
            await _unitOfWork.SaveChangesAsync();

            return Produto;
        }
        public async Task DeleteAsync(int id)
        {
            var produto = await _unitOfWork.Produtos.GetByIdAsync(id);
            if (produto == null)
            {
                return;
            }

            _unitOfWork.Produtos.Delete(produto);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

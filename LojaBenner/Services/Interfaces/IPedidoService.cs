using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Enums;

namespace LojaBenner.Services.Interfaces
{
    public interface IPedidoService
    {
        Task ChangeStatus(int id, StatusPedido status);
        Task<bool> Create(PedidoCreateDto pedidoCreateDto);
        
        Task<List<PedidoResumoDto>> GetByPessoaId(int pessoaId);
    }
}
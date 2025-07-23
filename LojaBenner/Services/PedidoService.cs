using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Enums;
using LojaBenner.Infrastructure.UnitOfWork.Interfaces;
using LojaBenner.Services.Interfaces;

namespace LojaBenner.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PedidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<bool> Create(PedidoCreateDto pedidoCreateDto)
        {
            if (pedidoCreateDto.Itens == null)
                return false;

            await _unitOfWork.Pedidos.CriarPedidoAsync(pedidoCreateDto.PessoaId, pedidoCreateDto.Itens, pedidoCreateDto.FormaPagamento);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<PedidoResumoDto>> GetByPessoaId(int pessoaId)
        {
            var pedidos = await _unitOfWork.Pedidos.ListPorPessoaAsync(pessoaId);

            List<PedidoResumoDto> pedidoResumoDto =
                pedidos.Select(p => 
                    new PedidoResumoDto( 
                            p.Id,
                            p.DataVenda.ToString("dd/MM/yyyy"), 
                            p.FormaPagamento.ToString() ,
                            p.Status.ToString(),
                            p.ValorTotal
                        )
                    ).ToList();


            return pedidoResumoDto;
        }

        public async Task ChangeStatus(int id, StatusPedido status)
        {
            await _unitOfWork.Pedidos.AtualizarStatusAsync(id, status);
            await _unitOfWork.SaveChangesAsync();
        }

        

    }
}

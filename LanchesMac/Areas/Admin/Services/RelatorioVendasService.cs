using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;

        public RelatorioVendasService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<List<Pedido>> RelatorioVendaSimples(DateTime? minDate, DateTime? MaxDate)
        {
            var resultado = from obj in context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate);
            }
            if (MaxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= MaxDate);
            }

            return await resultado.Include(l => l.PedidoItens)
                                   .ThenInclude(l=> l.Lanche)
                                   .OrderByDescending(x=>x.PedidoEnviado)
                                   .ToListAsync();

        }
    }
}

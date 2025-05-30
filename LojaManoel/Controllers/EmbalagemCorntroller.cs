// Controllers/EmbalagemController.cs
using LojaManoel.Api.Models;
using LojaManoel.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaManoel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Rota base para este controller
    public class EmbalagemController : ControllerBase
    {
        private readonly EmbalagemService _embalagemService;

        // Injeção de Dependência via construtor
        public EmbalagemController(EmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
        }

        [HttpPost("processar")] // Rota específica para este método
        [ProducesResponseType(typeof(List<PedidoSaida>), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessarPedidos([FromBody] List<PedidoEntrada> pedidos)
        {
            if (pedidos == null || !pedidos.Any())
            {
                return BadRequest("A lista de pedidos não pode ser vazia.");
            }

            var resultados = new List<PedidoSaida>();
            foreach (var pedido in pedidos)
            {
                // Validação básica
                if (string.IsNullOrEmpty(pedido.IdPedido))
                {
                    // Retornar um BadRequest com uma mensagem específica ou logar o erro
                    return BadRequest($"Pedido inválido: 'IdPedido' não pode ser nulo ou vazio para o pedido com ID '{pedido.IdPedido}'.");
                }
                if (pedido.Produtos == null || !pedido.Produtos.Any())
                {
                    // Logar ou retornar um erro se um pedido não tiver produtos
                    Console.WriteLine($"Aviso: Pedido '{pedido.IdPedido}' não contém produtos. Será ignorado ou tratado como pedido sem itens.");
                    // Opcional: PedidoSaida vazio para este caso ou simplesmente continuar
                    continue;
                }

                
                var resultadoPedido = _embalagemService.ProcessarEmbalagem(pedido);
                resultados.Add(resultadoPedido);
            }

            return Ok(resultados); // Retorna a lista de resultados de embalagem
        }
      
    }
}
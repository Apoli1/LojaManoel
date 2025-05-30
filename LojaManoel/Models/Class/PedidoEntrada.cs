namespace LojaManoel.Api.Models
{
    public class PedidoEntrada
    {
        public string IdPedido { get; set; } // Use string para maior flexibilidade
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
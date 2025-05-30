namespace LojaManoel.Api.Models
{
    public class PedidoEntrada
    {
        public string ?IdPedido { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
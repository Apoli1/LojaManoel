namespace LojaManoel.Api.Models
{
    public class PedidoSaida
    {
        public string IdPedido { get; set; }
        public List<ResultadoEmbalagemCaixa> CaixasUtilizadas { get; set; } = new List<ResultadoEmbalagemCaixa>();
    }
}
// Models/ResultadoEmbalagemCaixa.cs
namespace LojaManoel.Api.Models
{
    public class ResultadoEmbalagemCaixa
    {
        public string ?NomeCaixaUsada { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        
        public List<ResultadoEmbalagemProduto> ProdutosEmpacotados { get; set; } = new List<ResultadoEmbalagemProduto>();
    }
}
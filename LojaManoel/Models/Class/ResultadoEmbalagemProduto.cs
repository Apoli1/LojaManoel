namespace LojaManoel.Api.Models
{
    public class ResultadoEmbalagemProduto
    {
        public string IdProduto { get; set; } // Usar o ID do Produto original
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
    }
}
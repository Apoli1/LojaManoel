// Models/Produto.cs
namespace LojaManoel.Api.Models
{
    public class Produto
    {
        public string Id { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Volume => Altura * Largura * Comprimento;

        // Retorna todas as 6 orientações possíveis do produto
        public List<Tuple<decimal, decimal, decimal>> GetPermutacoesDimensao()
        {
            var dims = new List<Tuple<decimal, decimal, decimal>>();
            dims.Add(Tuple.Create(Altura, Largura, Comprimento));
            dims.Add(Tuple.Create(Altura, Comprimento, Largura));
            dims.Add(Tuple.Create(Largura, Altura, Comprimento));
            dims.Add(Tuple.Create(Largura, Comprimento, Altura));
            dims.Add(Tuple.Create(Comprimento, Altura, Largura));
            dims.Add(Tuple.Create(Comprimento, Largura, Altura));
            return dims.Distinct().ToList(); // Remove duplicatas se houver dimensões iguais
        }
    }
}
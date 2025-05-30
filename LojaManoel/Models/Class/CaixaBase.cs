namespace LojaManoel.Api.Models
{
    public class CaixaBase
    {
        public string Nome { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Volume => Altura * Largura * Comprimento;

        // Opcional: Construtor para facilitar a criação
        public CaixaBase(string nome, decimal altura, decimal largura, decimal comprimento)
        {
            Nome = nome;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }
    }
}
// Models/CaixaEmProcessamento.cs
using System; // Adicione este using se não estiver presente
using System.Collections.Generic;
using System.Linq; // Adicione este using se não estiver presente

namespace LojaManoel.Api.Models
{
    public class CaixaEmProcessamento
    {
        public string NomeCaixaBase { get; }
        public decimal AlturaOriginal { get; }
        public decimal LarguraOriginal { get; }
        public decimal ComprimentoOriginal { get; }

        public decimal VolumeDisponivel { get; private set; } // Esta é a capacidade restante

        public List<Produto> ProdutosEmpacotados { get; } = new List<Produto>();

        public CaixaEmProcessamento(CaixaBase baseCaixa)
        {
            NomeCaixaBase = baseCaixa.Nome;
            AlturaOriginal = baseCaixa.Altura;
            LarguraOriginal = baseCaixa.Largura;
            ComprimentoOriginal = baseCaixa.Comprimento;
            VolumeDisponivel = baseCaixa.Volume;
        }

        // Tenta adicionar um produto a esta caixa.
        // Retorna true se o produto couber e for adicionado, false caso contrário.
        public bool TentarAdicionarProduto(Produto produto)
        {
            // 1. Primeiro, checa se há volume suficiente
            if (produto.Volume > VolumeDisponivel)
            {
                return false; // Não há volume suficiente na caixa
            }

            // 2. Segundo, checa se o produto cabe em ALGUMA orientação dentro das dimensões da caixa.
            // Esta é a validação DIMENSIONAL, considerando rotações.
            bool cabeDimensionalmente = false;
            foreach (var perm in produto.GetPermutacoesDimensao())
            {
                decimal pAltura = perm.Item1;
                decimal pLargura = perm.Item2;
                decimal pComprimento = perm.Item3;

                // Se uma das permutações do produto cabe DENTRO das dimensões originais da caixa.
                // AQUI É A PARTE CRÍTICA QUE VAI IMPEDIR O PROD-004 DE ENTRAR NA CAIXA 1
                if (pAltura <= AlturaOriginal && pLargura <= LarguraOriginal && pComprimento <= ComprimentoOriginal)
                {
                    cabeDimensionalmente = true;
                    break; // Encontrou uma orientação que cabe
                }
            }

            if (!cabeDimensionalmente)
            {
                return false; // Produto não cabe dimensionalmente, mesmo com rotação
            }

            // Se passou nos dois testes (volume e dimensional), adiciona o produto
            ProdutosEmpacotados.Add(produto);
            VolumeDisponivel -= produto.Volume; // Diminui o volume disponível

            return true; // Produto adicionado com sucesso
        }
    }
}
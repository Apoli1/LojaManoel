// Services/EmbalagemService.cs
using LojaManoel.Api.Models;
using System.Linq;
using System.Collections.Generic;

namespace LojaManoel.Api.Services
{
    public class EmbalagemService
    {
        public PedidoSaida ProcessarEmbalagem(PedidoEntrada pedidoEntrada)
        {
            var pedidoSaida = new PedidoSaida
            {
                IdPedido = pedidoEntrada.IdPedido,
                CaixasUtilizadas = new List<ResultadoEmbalagemCaixa>()
            };

            // 1. Ordene os produtos do maior para o menor volume (ou outra dimensão)
            var produtosOrdenados = pedidoEntrada.Produtos
                                                .OrderByDescending(p => p.Volume) 
                                                .ToList();

            // Lista de caixas que estão sendo usadas e que ainda têm espaço (CaixaEmProcessamento)
            var caixasAbertas = new List<CaixaEmProcessamento>();

            foreach (var produto in produtosOrdenados)
            {
                bool produtoEmbalado = false;

                // 2. Tente encaixar o produto em uma caixa JÁ UTILIZADA que ainda tenha espaço
                foreach (var caixaEmUso in caixasAbertas)
                {
                    if (caixaEmUso.TentarAdicionarProduto(produto))
                    {
                        produtoEmbalado = true;
                        break; // Produto encaixado, vá para o próximo produto
                    }
                }

                // 3. Se o produto não foi embalado em nenhuma caixa existente, abra uma nova
                if (!produtoEmbalado)
                {
                    CaixaBase caixaSelecionadaBase = null;

                    // Encontra a menor caixa BASE DISPONÍVEL que o produto cabe
                    // Ordena as caixas base pelo volume para tentar usar a menor possível (otimização de caixas)
                    var caixasBaseOrdenadas = CaixaInventario.CaixasPadrao
                                                             .OrderBy(c => c.Volume)
                                                             .ToList();

                    foreach (var caixaBase in caixasBaseOrdenadas)
                    {
                        // Temporariamente cria uma caixa em processamento para testar se o produto CABE nela.
                        var tempCaixa = new CaixaEmProcessamento(caixaBase);
                        if (tempCaixa.TentarAdicionarProduto(produto)) // Se o produto cabe na caixa base VAZIA
                        {
                            caixaSelecionadaBase = caixaBase;
                            // Adicione o produto à tempCaixa, que agora se torna a "nova caixa aberta"
                            caixasAbertas.Add(tempCaixa); // Adiciona a caixa TEMPORÁRIA (já com o produto)
                            produtoEmbalado = true;
                            break;
                        }
                    }

                    if (!produtoEmbalado)
                    {
                        // CENÁRIO CRÍTICO: Produto não cabe em NENHUMA CAIXA DISPONÍVEL.
                        // Por enquanto, vamos logar. Em um sistema real, você retornaria um erro.
                        Console.WriteLine($"AVISO: Produto '{produto.Id}' (V:{produto.Volume} A:{produto.Altura} L:{produto.Largura} C:{produto.Comprimento}) não cabe em NENHUMA caixa disponível.");
                        // Você pode adicionar uma "Caixa de Exceção" ou marcá-lo como "Não Empacotável"
                        // Por agora, ele simplesmente não será incluído na saída.
                    }
                }
            }

            // 4. Converta as CaixasEmProcessamento para o formato de saída (ResultadoEmbalagemCaixa)
            foreach (var caixaEmProcessamento in caixasAbertas)
            {
                var resultadoCaixa = new ResultadoEmbalagemCaixa
                {
                    NomeCaixaUsada = caixaEmProcessamento.NomeCaixaBase,
                    Altura = caixaEmProcessamento.AlturaOriginal, // Use as dimensões originais da caixa
                    Largura = caixaEmProcessamento.LarguraOriginal,
                    Comprimento = caixaEmProcessamento.ComprimentoOriginal,
                    ProdutosEmpacotados = caixaEmProcessamento.ProdutosEmpacotados.Select(p => new ResultadoEmbalagemProduto
                    {
                        IdProduto = p.Id,
                        Altura = p.Altura,
                        Largura = p.Largura,
                        Comprimento = p.Comprimento
                    }).ToList()
                };
                pedidoSaida.CaixasUtilizadas.Add(resultadoCaixa);
            }

            return pedidoSaida;
        }
    }
}
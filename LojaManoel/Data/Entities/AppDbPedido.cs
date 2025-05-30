// Data/Entities/Pedido.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Para [NotMapped]

namespace LojaManoel.Data.Entities // NOVO NAMESPACE: LojaManoel.Data.Entities
{
    [Table("Pedidos")] // Nome da tabela no banco de dados
    public class Pedido
    {
        public int Id { get; set; } // Chave primária do DB
        public string ?IdPedidoExterno { get; set; } // O IdPedido do seu JSON
        public DateTime DataCriacao { get; set; }

        [NotMapped] // Não mapeia esta propriedade para o banco de dados
        public List<Models.Produto> ProdutosOriginaisInput { get; set; } // Referência aos produtos originais do input DTO

        public List<ProdutoEntity> ProdutosOriginais { get; set; } // Produtos como vieram no pedido (agora como entidades DB)
        public List<CaixaEntity> Caixas { get; set; } // Caixas usadas para este pedido
    }
}

// Data/Entities/ProdutoEntity.cs (Renomeamos para evitar conflito com Models.Produto)
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaManoel.Data.Entities // NOVO NAMESPACE
{
    [Table("ProdutosOriginais")] // Nome da tabela no banco de dados
    public class ProdutoEntity // Renomeada para ProdutoEntity
    {
        public int Id { get; set; } // Chave primária do DB
        public string IdProdutoExterno { get; set; } // O Id do produto do seu JSON
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}

// Data/Entities/CaixaEntity.cs (Renomeada para evitar conflito com Models.CaixaBase)
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaManoel.Data.Entities // NOVO NAMESPACE
{
    [Table("CaixasUtilizadas")] // Nome da tabela no banco de dados
    public class CaixaEntity // Renomeada para CaixaEntity
    {
        public int Id { get; set; } // Chave primária do DB
        public string NomeCaixaBase { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public List<ProdutoEmbaladoEntity> ProdutosEmbalados { get; set; }
    }
}

// Data/Entities/ProdutoEmbaladoEntity.cs (Renomeada para evitar conflito)
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaManoel.Data.Entities // NOVO NAMESPACE
{
    [Table("ProdutosEmbaladosNaCaixa")] // Nome da tabela no banco de dados
    public class ProdutoEmbaladoEntity // Renomeada para ProdutoEmbaladoEntity
    {
        public int Id { get; set; } // Chave primária do DB
        public string IdProdutoExterno { get; set; } // O Id do produto do seu JSON
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public int CaixaId { get; set; }
        public CaixaEntity Caixa { get; set; }
    }
}
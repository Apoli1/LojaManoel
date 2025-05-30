# 📦 Loja do Seu Manoel: API de Embalagem Automatizada

## ✨ Visão Geral do Projeto

Este projeto implementa uma **API Web em .NET 8** para automatizar o processo de embalagem de pedidos da loja de jogos online do Seu Manoel. A API recebe uma lista de pedidos, cada um contendo produtos com suas dimensões. Para cada pedido, ela determina a melhor combinação de caixas disponíveis para embalar os produtos, visando **otimizar o espaço e minimizar o número de caixas utilizadas**.

A solução é totalmente **dockerizada**, utilizando **SQL Server** como banco de dados e expõe sua funcionalidade via **Swagger UI** para fácil testabilidade.

## 🚀 Requisitos e Tecnologias Utilizadas

* **Linguagem & Framework:** C# (.NET 8)
* **API:** ASP.NET Core Web API
* **Banco de Dados:** SQL Server
* **Containerização & Orquestração:** Docker e Docker Compose
* **ORM:** Entity Framework Core (EF Core)
* **Documentação da API:** Swagger/OpenAPI
* **Algoritmo de Empacotamento:** Heurística First Fit Decreasing (FFD) com rotação de itens.

## ⚙️ Pré-requisitos

Para rodar a aplicação localmente, você precisará apenas ter o **Docker Desktop** instalado e em execução.

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## 🏁 Como Rodar a Aplicação

Siga estes passos para iniciar a API e o banco de dados via Docker Compose:

1.  **Clone o Repositório:**
    ```bash
    git clone [SEU_LINK_DO_REPOSITORIO_GITHUB_AQUI]
    cd [SUA_PASTA_DO_PROJETO_LOJAMANOEL]
    ```
    (Certifique-se de estar na pasta que contém `LojaManoel.csproj`, `Dockerfile` e `docker-compose.yml`).

2.  **Inicie os Contêineres:**
    No terminal, execute o seguinte comando. Na primeira execução, o Docker baixará as imagens necessárias e construirá a imagem da sua API, o que pode levar alguns minutos.
    ```bash
    docker-compose up --build
    ```
    * Aguarde até que os logs no terminal indiquem que tanto o serviço `sqlserver_db` quanto o `lojamanoel_api` estão iniciados e escutando (`Now listening on: http://[::]:80`). O SQL Server pode levar um pouco mais de tempo para inicializar completamente.

## 🧪 Como Testar a API

A API está documentada e pode ser testada via Swagger UI:

1.  **Acesse o Swagger UI:**
    Com os contêineres rodando, abra seu navegador e acesse:
    ```
    http://localhost:5149/swagger
    ```

2.  **Teste o Endpoint de Embalagem:**
    * No Swagger UI, localize e expanda o endpoint `POST /api/Embalagem/processar`.
    * Clique em **"Try it out"**.
    * No campo "Request body", cole o seguinte JSON de exemplo:

    ```json
    [
      {
        "idPedido": "PEDIDO-001",
        "produtos": [
          {
            "id": "PROD-001",
            "altura": 10,
            "largura": 10,
            "comprimento": 10
          },
          {
            "id": "PROD-002",
            "altura": 20,
            "largura": 30,
            "comprimento": 15
          },
          {
            "id": "PROD-003",
            "altura": 5,
            "largura": 5,
            "comprimento": 5
          }
        ]
      },
      {
        "idPedido": "PEDIDO-002",
        "produtos": [
          {
            "id": "PROD-004",
            "altura": 70,
            "largura": 40,
            "comprimento": 30
          },
          {
            "id": "PROD-005",
            "altura": 25,
            "largura": 35,
            "comprimento": 10
          }
        ]
      }
    ]
    ```
    * Clique em **"Execute"**.
    * A resposta (Response body) mostrará as caixas utilizadas e os produtos empacotados para cada pedido.

## 📦 Caixas Disponíveis

As dimensões das caixas pré-fabricadas do Seu Manoel são:

* **Caixa 1:** 30 x 40 x 80 cm (A x L x C)
* **Caixa 2:** 80 x 50 x 40 cm (A x L x C)
* **Caixa 3:** 50 x 80 x 60 cm (A x L x C)

## 💡 Observações e Decisões de Design

* **Algoritmo de Empacotamento:** Foi implementada uma heurística "First Fit Decreasing" (FFD). Produtos são ordenados por volume decrescente e tentados a encaixar na primeira caixa disponível que os acomoda, considerando **todas as suas 6 possíveis rotações**. Isso ajuda a otimizar o uso do espaço das caixas existentes.
* **Separação de Preocupações:** Os modelos de domínio (DTOs para entrada/saída) foram separados das entidades de banco de dados (que são mapeadas pelo Entity Framework Core), promovendo um design mais limpo e manutenível.
* **Persistência:** A aplicação salva os dados dos pedidos de entrada e os resultados do empacotamento (quais produtos foram em quais caixas) no SQL Server utilizando EF Core e migrações. O banco de dados é criado e atualizado automaticamente na inicialização da aplicação (em ambiente de desenvolvimento).
* **Robustez do Docker:** O ambiente Docker foi configurado para ser resiliente, tratando a inicialização do SQL Server e a comunicação entre os serviços.

* ## 🤝 Ferramentas Auxiliares

* **Durante o desenvolvimento e depuração deste projeto, utilizei **ferramentas de Inteligência Artificial** (como um assistente de codificação/depuração) para acelerar o aprendizado e a resolução de problemas complexos relacionados à configuração de ambiente e sintaxe. Essa abordagem permitiu focar na lógica de negócio e na compreensão aprofundada dos desafios.
---

### ** Informações Adicionais**

* **Nome:** Athyrson G. Apolinário
* **LinkedIn:** https://www.linkedin.com/in/athyrson-ga/
* **GitHub:** https://github.com/Apoli1
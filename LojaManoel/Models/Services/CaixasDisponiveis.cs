namespace LojaManoel.Api.Models // ou LojaManoel.Api.Services
{
    public static class CaixaInventario
    {
        public static List<CaixaBase> CaixasPadrao { get; } = new List<CaixaBase>
        {
            new CaixaBase("Caixa 1", 30, 40, 80),
            new CaixaBase("Caixa 2", 80, 50, 40),
            new CaixaBase("Caixa 3", 50, 80, 60)
        };
    }
}
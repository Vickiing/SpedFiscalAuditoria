using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Documentos;

public sealed class ItemDocumentoFiscalTestes
{
    [Fact]
    public void Deve_calcular_total_do_item()
    {
        var item = new ItemDocumentoFiscal("Cabo", new Ncm("12345678"), 3, 10m);

        Assert.Equal(30m, item.Total);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_rejeitar_quantidade_invalida(decimal quantidade)
    {
        var excecao = Assert.Throws<RegraNegocioExcecao>(() => new ItemDocumentoFiscal("Cabo", new Ncm("12345678"), quantidade, 10m));

        Assert.Contains("quantidade", excecao.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Deve_rejeitar_valor_unitario_negativo()
    {
        var excecao = Assert.Throws<RegraNegocioExcecao>(() => new ItemDocumentoFiscal("Cabo", new Ncm("12345678"), 1, -1m));

        Assert.Contains("valor unitario", excecao.Message, StringComparison.OrdinalIgnoreCase);
    }
}

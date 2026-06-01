using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Documentos;

public sealed class NcmTestes
{
    [Fact]
    public void Deve_criar_ncm_valido()
    {
        var ncm = new Ncm("12345678");

        Assert.Equal("12345678", ncm.Codigo);
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("1234567A")]
    public void Deve_rejeitar_codigo_invalido(string codigo)
    {
        var excecao = Assert.Throws<RegraNegocioExcecao>(() => new Ncm(codigo));

        Assert.Contains("NCM", excecao.Message, StringComparison.OrdinalIgnoreCase);
    }
}

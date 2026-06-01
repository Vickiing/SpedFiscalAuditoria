using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Documentos;

public sealed class DocumentoFiscalTestes
{
    [Fact]
    public void Deve_somar_itens_do_documento()
    {
        var itens = new[]
        {
            new ItemDocumentoFiscal("Produto A", new Ncm("12345678"), 2, 10m),
            new ItemDocumentoFiscal("Produto B", new Ncm("87654321"), 1, 20m)
        };

        var documento = new DocumentoFiscal("NF-1", "Fornecedor", itens, 40m);

        Assert.Equal(40m, documento.SomarTotalItens());
        Assert.Equal(2, documento.Itens.Count);
    }

    [Theory]
    [InlineData("", "Fornecedor")]
    [InlineData("NF-1", "")]
    public void Deve_rejeitar_campos_obrigatorios(string numero, string emitente)
    {
        var itens = new[] { new ItemDocumentoFiscal("Produto", new Ncm("12345678"), 1, 10m) };

        Assert.Throws<RegraNegocioExcecao>(() => new DocumentoFiscal(numero, emitente, itens, 10m));
    }

    [Fact]
    public void Deve_permitir_documento_sem_itens()
    {
        var documento = new DocumentoFiscal("NF-1", "Fornecedor", Array.Empty<ItemDocumentoFiscal>(), 0m);

        Assert.Empty(documento.Itens);
        Assert.Equal(0m, documento.SomarTotalItens());
    }
}

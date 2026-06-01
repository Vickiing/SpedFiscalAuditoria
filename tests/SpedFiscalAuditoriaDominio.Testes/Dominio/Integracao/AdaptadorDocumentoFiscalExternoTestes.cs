using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.Integracao;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Integracao;

public sealed class AdaptadorDocumentoFiscalExternoTestes
{
    [Fact]
    public void Deve_converter_documento_externo_para_dominio()
    {
        var externo = new DocumentoFiscalExterno
        {
            Numero = "NF-1",
            Emitente = "Fornecedor",
            TotalInformado = 20m,
            Itens =
            [
                new ItemDocumentoFiscalExterno
                {
                    Descricao = "Produto",
                    Ncm = "12345678",
                    Quantidade = 2,
                    ValorUnitario = 10m
                }
            ]
        };

        var documento = AdaptadorDocumentoFiscalExterno.ParaDocumentoFiscal(externo);

        Assert.Equal("NF-1", documento.Numero);
        Assert.Equal(20m, documento.SomarTotalItens());
    }

    [Fact]
    public void Deve_rejeitar_ncm_invalido_no_documento_externo()
    {
        var externo = new DocumentoFiscalExterno
        {
            Numero = "NF-1",
            Emitente = "Fornecedor",
            TotalInformado = 10m,
            Itens =
            [
                new ItemDocumentoFiscalExterno
                {
                    Descricao = "Produto",
                    Ncm = "ABC",
                    Quantidade = 1,
                    ValorUnitario = 10m
                }
            ]
        };

        Assert.Throws<RegraNegocioExcecao>(() => AdaptadorDocumentoFiscalExterno.ParaDocumentoFiscal(externo));
    }
}
